﻿using Techrunch.TecVas.Data;
using Techrunch.TecVas.Entities;
using Techrunch.TecVas.Entities.BusinessAccount;
using Techrunch.TecVas.Entities.Common;
using Techrunch.TecVas.Entities.Epurse;
using Techrunch.TecVas.Entities.Inventory;
using Techrunch.TecVas.Entities.Partner;
using Techrunch.TecVas.Entities.Product;
using Techrunch.TecVas.Entities.ViewModels;
using Techrunch.TecVas.Services.Authentication;
using Techrunch.TecVas.Services.QueService;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Techrunch.TecVas.Services.TransactionRecordService
{
    public class TransactionRecordService : ITransactionRecordService
    {
        private readonly ILogger<ITransactionRecordService> _logger;
        private readonly IRepository<TopUpTransactionLog> _requestsRepo;
        private readonly IRepository<EpurseAccountMaster> _epurserepo;
        private readonly IRepository<BusinessAccount> _partnerRepo;
        private readonly IRepository<StockDetails> _stockRepo;
        private readonly IRepository<VtuProducts> _productsRepo;
        private readonly IRepository<EpurseAcctTransactions> _epursetransRepo;
        private readonly IRepository<ApiCredentials> _apicredRepo;
        private readonly IRepository<StockMaster> _stockmasterRepo;
        private readonly IRepository<PartnerServiceProvider> _partnerServicesRepo;
        private readonly IRepository<CarrierPrefix> _carrierRepo;
        private readonly IRepository<PartnerStockSalesHistory> _stockhistRepo;
        private readonly IRepository<VtuProducts> _telcoprpductRepo;

        public TransactionRecordService(
            ILogger<ITransactionRecordService> logger,

            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _requestsRepo = unitOfWork.GetRepository<TopUpTransactionLog>();
            _epurserepo = unitOfWork.GetRepository<EpurseAccountMaster>();
            _partnerRepo = unitOfWork.GetRepository<BusinessAccount>();
            _stockRepo = unitOfWork.GetRepository<StockDetails>();
            _productsRepo = unitOfWork.GetRepository<VtuProducts>();
            _epursetransRepo = unitOfWork.GetRepository<EpurseAcctTransactions>();
            _apicredRepo = unitOfWork.GetRepository<ApiCredentials>();
            _stockmasterRepo = unitOfWork.GetRepository<StockMaster>();
            _partnerServicesRepo = unitOfWork.GetRepository<PartnerServiceProvider>();
            _carrierRepo = unitOfWork.GetRepository<CarrierPrefix>();
            _stockhistRepo = unitOfWork.GetRepository<PartnerStockSalesHistory>();
            _telcoprpductRepo = unitOfWork.GetRepository<VtuProducts>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="transreference"></param>
        /// <returns></returns>
        public bool IsTransactionExist(string transreference, int partnerId)
        {
            _logger.LogInformation($"Checking if transactionexists from Database : {transreference}");
            var requestObkect = _requestsRepo.GetQueryable()

                .Where(a => a.transref == transreference && a.PartnerId == partnerId);
            return requestObkect.Count() > 0;

        }
        public async Task<ApiCredentials> GetPartnerbyAPIkey(string apiKey)
        {
            //int partnerid = 0;
            ApiCredentials creds = null;
            try
            {
                _logger.LogInformation($"Checking GetPartnerbyAPIkey from Database");                

                creds = _apicredRepo.GetQueryable()

                    .Where(a => a.ApiKey == apiKey && a.Active == true).FirstOrDefault();
                
                //partnerid = requestObject.Id;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get partnerId by apikey");
                //return creds;
            }

            return creds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rechargeRequest"></param>
        /// <returns></returns>
        public async Task<bool> RecordTransaction(RechargeRequest rechargeRequest, int partnerId)
        {
            bool transactionStatus = false;
            _logger.LogInformation($"Saving transaction record : {JsonConvert.SerializeObject(rechargeRequest)} from partnerID {partnerId}");
            string serviceprovidername = Enum.GetName(typeof(ServiceProvider), rechargeRequest.ServiceProviderId);
            var partner = await GetPatnerById(partnerId);
            if(partner!=null)
            {
                string partnerName = partner.PartnerName;
                _logger.LogInformation($"fetching available prod categories for  partner : {partnerName} ");

                int prepaidpayentsCategory = (int)ProductCategory.Prepaid;
                var partnerService = _partnerServicesRepo.GetQueryable()
                .Where(a => a.PartnerId == partnerId && a.ServiceProviderId == rechargeRequest.ServiceProviderId
                && a.ProductCategoryid == prepaidpayentsCategory).FirstOrDefault();

                decimal commission = 0;

                if (partnerService != null)
                {
                    _logger.LogInformation($"available prod categories for  partner : {partnerName} ");
                    commission = partnerService.CommissionPct == null ? 0 : (decimal)partnerService.CommissionPct;
                }
                decimal settlementAmt = commission == 0 ? rechargeRequest.rechargeAmount : rechargeRequest.rechargeAmount - (commission / 100 * rechargeRequest.rechargeAmount);

                TopUpTransactionLog topUpRequest = new TopUpTransactionLog
                {
                    tran_date = DateTime.Now,
                    transamount = rechargeRequest.rechargeAmount,
                    transref = rechargeRequest.TransactionReference,
                    transtype = rechargeRequest.TransactionType,
                    channelid = rechargeRequest.ChannelId,
                    msisdn = rechargeRequest.PhoneNumber,
                    productid = rechargeRequest.ProductId,
                    serviceproviderid = rechargeRequest.ServiceProviderId,
                    sourcesystem = partnerName,
                    serviceprovidername = serviceprovidername,
                    PartnerId = partnerId,
                    CountRetries = 0,
                    IsProcessed = 0,
                    TransactionStatus = 0,
                    SettlementAmount = settlementAmt,
                    ThreadNo = 1


                };
                var requestObkect = await _requestsRepo.AddAsync(topUpRequest);
                await _requestsRepo.SaveAsync();

            }



            return transactionStatus;

        }
        public async Task<EpurseAccountMaster> CreditEpurseAccount(AccountTopUpRequest accountTopUpRequest)
        {


            //int acctcount = _epurserepo.GetQueryable()
            //    .Where(a => a.PartnerId == accountTopUpRequest.PartnerId).Count();
            var epurseacct = _epurserepo.GetQueryable()
                .Where(a => a.PartnerId == accountTopUpRequest.PartnerId && a.ProductcategoryId == accountTopUpRequest.ProductCategoryId).FirstOrDefault();
            if(epurseacct != null)
            {
                epurseacct.LastCreditDate = DateTime.Now;
                epurseacct.CreatedBy = accountTopUpRequest.CreatedBy;
                epurseacct.AuthorisedBy = epurseacct.AuthorisedBy;
                epurseacct.MainAcctBalance = epurseacct.MainAcctBalance + accountTopUpRequest.Amount;

                await _epurserepo.UpdateAsync(epurseacct);
                await _epurserepo.SaveAsync();
            }
            
            return epurseacct;
        }

        #region Epurse
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PartnerId"></param>
        /// <returns></returns>
        public List<EpurseAccountMaster> GetEpurseByPartnerId(int PartnerId)
        {

            var epurseAccounts = _epurserepo.GetQueryable()
                .Where(a => a.PartnerId == PartnerId ).ToList();


            return epurseAccounts;
        }
        public EpurseAccountMaster GetEpurseByPartnerIdCategoryId(int PartnerId, int productcategoryId)
        {
            _logger.LogInformation($"Get Epurse data  for partnerId: {PartnerId} productcategory :{productcategoryId}");

            var epurseAccounts = _epurserepo.GetQueryable()
                .Where(a => a.PartnerId == PartnerId && a.ProductcategoryId == productcategoryId).FirstOrDefault();


            return epurseAccounts;
        }
        public async Task<IEnumerable<EpurseAccountMaster>> GetEpurseAccounts()
        {

            var epurseAccounts = await _epurserepo.GetAllAsync();

            return epurseAccounts;
        }



        public bool IsPartnerExist(int partnerId)
        {
            _logger.LogInformation($"Checking if IspartnerExists from Database : {partnerId}");
            var requestObkect = _partnerRepo.GetQueryable()

                .Where(a => a.PartnerId == partnerId); ;
            return requestObkect.Count() > 0;

        }
        public bool IsEpurseExist(int partnerId, int tenantId)
        {
            _logger.LogInformation($"Checking if IsEpurseExist from Database : {partnerId}");
            var requestObkect = _epurserepo.GetQueryable()

                .Where(a => a.PartnerId == partnerId && a.TenantId == tenantId); 
            return requestObkect.Count() > 0;

        }
        public async Task<EpurseAccountMaster> CreateEpurseAccount(EpurseAccountMaster epurseAccount, int prodCat)
        {
            var lastacct = _epurserepo.GetQueryable().OrderByDescending(a => a.AcctNo).FirstOrDefault();
               // .Where(a => a.PartnerId == epurseAccount.PartnerId ).OrderByDescending(a=>a.AcctNo).FirstOrDefault(); //&& a.ProductcategoryId == prodCat
            int acctNo = 0;

            
            if(lastacct == null)
            {
                _logger.LogInformation($"No  account found for partnerId:{epurseAccount.PartnerId}");
                epurseAccount.AcctNo = "1000001";
            }
            else
            {
                acctNo = int.Parse(lastacct.AcctNo) + 1;
                epurseAccount.AcctNo = acctNo.ToString();
                _logger.LogInformation($"Next  account number for partnerId:{epurseAccount.PartnerId} : acctNo");
            }

            epurseAccount.MainAcctBalance = 0;
            epurseAccount.RewardPoints = 0;
            epurseAccount.CommissionAcctBalance = 0;
            epurseAccount.CreatedBy = epurseAccount.CreatedBy;
            epurseAccount.AuthorisedBy = epurseAccount.AuthorisedBy;
            epurseAccount.ProductcategoryId = prodCat;
            epurseAccount.CreatedDate = DateTime.Now;
            _logger.LogInformation($"Creating account number: {epurseAccount.AcctNo}");

            await _epurserepo.AddAsync(epurseAccount);
            await _epurserepo.SaveAsync();


            return epurseAccount;
        }
        #endregion

        public async Task<IEnumerable<StockBalanceView>> GetStockbalancesbyPartnerId(int partnerId)
        {
            //var list1 = dbEntities.People.
            //GroupBy(m => m.PersonType).
            //Select(c =>
            //    new
            //    {
            //        Type = c.Key,
            //        Max = c.Max(),
            //    });

            var balances = _stockmasterRepo.GetQueryable()
                .Where(a => a.PartnerId == partnerId)
                
                .Select(g =>
               new StockBalanceView
               {
                   ProductCategoryId = 2,
                   ServiceProviderId = g.ServiceProviderId,
                   PartnerId = partnerId,
                   Qoh = g.QuantityOnHand
               });

            return balances;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StockBalanceView>> GetStockbalancesbyPartnerId_v1(int partnerId)
        {
            //var list1 = dbEntities.People.
            //GroupBy(m => m.PersonType).
            //Select(c =>
            //    new
            //    {
            //        Type = c.Key,
            //        Max = c.Max(),
            //    });

            var balances = _stockRepo.GetQueryable()
                .Where(a => a.partner_id == partnerId)
                .GroupBy(a => a.service_provider_id)
                .Select( g => 
                new StockBalanceView
                {
                    ProductCategoryId = 2,
                    ServiceProviderId = g.Key,
                    PartnerId = partnerId,
                    Qoh = g.Sum( a=>a.quantity) 
                });

            return balances;
        }
        public async Task<bool> StockSales(int partnerId, int serviceproviderId, decimal txnAmt, string transref)
        {
            bool retstatus = false;
            
            //Decrease the QOH
            try
            {
                _logger.LogInformation($"Start debit stock master for partnerId: {partnerId}, serviceprovider: {serviceproviderId}");
                int qty = (int)txnAmt;
                StockMaster stockObj = _stockmasterRepo.GetQueryable(a => a.PartnerId == partnerId && a.ServiceProviderId == serviceproviderId).FirstOrDefault();
                int qoh_before = stockObj.QuantityOnHand;
                stockObj.QuantityOnHand = stockObj.QuantityOnHand - qty;
                _logger.LogInformation($"stock master for partnerId: {partnerId}, serviceprovider: {serviceproviderId} : NEWBALANCE: {stockObj.QuantityOnHand}");
                

                PartnerStockSalesHistory partnerStockSalesHistory = new PartnerStockSalesHistory
                {
                    PartnerId = stockObj.PartnerId,
                    ServiceProviderId = stockObj.ServiceProviderId,
                    QuantityOnHand_Before = qoh_before,
                    QuantityOnHand_After = qoh_before - qty,
                    TenantId = stockObj.TenantId,
                    Transref = transref
                };
                await _stockhistRepo.AddAsync(partnerStockSalesHistory);
                await _stockmasterRepo.UpdateAsync(stockObj);


                retstatus = true;
                _logger.LogInformation($"End debit stock master for partnerId: {partnerId}, serviceprovider: {serviceproviderId} newvalue : {stockObj.QuantityOnHand}");

            }

            catch(Exception ex)
            {
                _logger.LogError($"Failed to debit partner for txn ID: {transref}");
            }            
            
            return retstatus;
        }
        public async Task<bool> PurchaseStock(StockPurchaseOrder stockPurchaseRequest, bool addCommision)
        {
            int rowcount = 0;
            var epurseacctObj = _epurserepo.GetQueryable()
                .Where(a => a.PartnerId == stockPurchaseRequest.PartnerId && a.ProductcategoryId == stockPurchaseRequest.ProductCategoryId).FirstOrDefault();

            

            foreach (var orderLine in stockPurchaseRequest.items)
            {
                var product = _productsRepo.GetQueryable()
                .Where(a => a.ServiceProviderId == orderLine.ServiceProviderId && a.ProductType == 1).FirstOrDefault();

               

                //Create a transaction on the partner's account
                var epurseaccttrans = new EpurseAcctTransactions
                {
                    PartnerId = stockPurchaseRequest.PartnerId,
                    DrCr = "D",
                    TranDate = DateTime.Now,
                    TranDesc = "Stock Purchase",
                    TenantId = stockPurchaseRequest.tenantId,
                    TranAmount = orderLine.Quantity * product.Price,
                    UserId = stockPurchaseRequest.UserId,
                    AccountNo = epurseacctObj.AcctNo,
                    ProductCode = product.ProductId,
                    ServiceProviderId = orderLine.ServiceProviderId

                };
                _epursetransRepo.Add(epurseaccttrans);
                //create an inventory details record

                var stockentry = new StockDetails
                {
                    partner_id = stockPurchaseRequest.PartnerId,
                    price = 0,
                    quantity = orderLine.Quantity,
                    service_provider_id = orderLine.ServiceProviderId,
                    tenant_id = stockPurchaseRequest.tenantId,
                    trans_date = DateTime.Now,
                    trans_type_id = 1,
                    product_id = product.ProductId,
                    user_id = stockPurchaseRequest.UserId
                };
                _stockRepo.Add(stockentry);

                //Increase the QOH
                StockMaster stockObj = _stockmasterRepo.GetQueryable(a => a.PartnerId == stockPurchaseRequest.PartnerId && a.ServiceProviderId == orderLine.ServiceProviderId).FirstOrDefault();
                if (stockObj == null)
                {
                    StockMaster masterrecord = new StockMaster
                    {
                        ServiceProviderId = orderLine.ServiceProviderId,
                        PartnerId = stockPurchaseRequest.PartnerId,
                        TenantId = stockPurchaseRequest.tenantId,
                        QuantityOnHand = orderLine.Quantity
                    };
                    //stockObj.partner_id = stockPurchaseRequest.PartnerId;
                    //stockObj.tenant_id = stockPurchaseRequest.tenantId;
                    //stockObj.service_provider_id = orderLine.ServiceProviderId;
                    //stockObj.QuantityOnHand = orderLine.Quantity;

                    _logger.LogInformation($"transactionrecord adding stock master: {JsonConvert.SerializeObject(masterrecord)}");
                    await _stockmasterRepo.AddAsync(masterrecord);

                }
                else
                {
                    
                    stockObj.QuantityOnHand = stockObj.QuantityOnHand + orderLine.Quantity;
                    _logger.LogInformation($"transactionrecord updating stock master: {JsonConvert.SerializeObject(stockObj)}");
                    await _stockmasterRepo.UpdateAsync(stockObj);
                }



                if (addCommision)
                {
                    decimal commisionpct = 0;
                    var commisionObj = _partnerServicesRepo.GetQueryable()
                   .Where(a => a.PartnerId == stockPurchaseRequest.PartnerId && a.ServiceProviderId == orderLine.ServiceProviderId).FirstOrDefault();

                    if(commisionObj != null && commisionObj.CommissionPct != null )
                    {
                        commisionpct = (decimal)commisionObj.CommissionPct;

                        if(commisionpct > 0)
                        {
                            int commisionQty = Convert.ToInt32(Math.Round((orderLine.Quantity * (commisionpct / 100))));
                            decimal commisionAmt = (orderLine.Quantity * (commisionpct / 100));
                            //Add the  partner's commision
                            var epurseCommitssion = new EpurseAcctTransactions
                            {
                                PartnerId = stockPurchaseRequest.PartnerId,
                                DrCr = "C",
                                TranDate = DateTime.Now,
                                TranDesc = "Commission for Stock Purchase",
                                TenantId = stockPurchaseRequest.tenantId,
                                TranAmount = commisionAmt * product.Price, //GetHashCode rate Laolu
                                UserId = stockPurchaseRequest.UserId,
                                AccountNo = epurseacctObj.AcctNo,
                                ProductCode = product.ProductId,
                                ServiceProviderId = orderLine.ServiceProviderId

                            };
                            _epursetransRepo.Add(epurseCommitssion);

                            var commisionEntry = new StockDetails
                            {
                                partner_id = stockPurchaseRequest.PartnerId,
                                price = 0,
                                quantity = commisionQty,
                                service_provider_id = orderLine.ServiceProviderId,
                                tenant_id = stockPurchaseRequest.tenantId,
                                trans_date = DateTime.Now,
                                trans_type_id = 1,
                                product_id = product.ProductId,
                                user_id = stockPurchaseRequest.UserId
                            };
                            _stockRepo.Add(commisionEntry);

                            var commisionStock = _stockmasterRepo.GetQueryable(a => a.PartnerId == stockPurchaseRequest.PartnerId && a.ServiceProviderId == orderLine.ServiceProviderId).FirstOrDefault();

                            if (commisionStock == null)
                            {
                                StockMaster masterrecord = new StockMaster
                                {
                                    ServiceProviderId = orderLine.ServiceProviderId,
                                    PartnerId = stockPurchaseRequest.PartnerId,
                                    TenantId = stockPurchaseRequest.tenantId,
                                    QuantityOnHand = commisionQty  //get commiison #tage
                                };
                                await _stockmasterRepo.AddAsync(masterrecord);

                            }
                            else
                            {
                                commisionStock.QuantityOnHand = stockObj.QuantityOnHand + commisionQty;  //get commiison #tage
                                await _stockmasterRepo.UpdateAsync(commisionStock);
                            }
                        }
                    }

                   

                }
                
                
                


                //debit the main account
                epurseacctObj.MainAcctBalance = epurseacctObj.MainAcctBalance - (orderLine.Quantity * product.Price);
                await _epurserepo.UpdateAsync(epurseacctObj);

            }

            rowcount = await _epursetransRepo.SaveAsync(); rowcount = 0;
            rowcount = await _epurserepo.SaveAsync(); rowcount = 0;
            rowcount = await _stockRepo.SaveAsync();
            rowcount = await _stockmasterRepo.SaveAsync();

            return true;


        }
        
        public async Task<BusinessAccount> GetPatnerById(int partnerId)
        {
            var partner = _partnerRepo.GetQueryable().Where(a => a.PartnerId == partnerId).FirstOrDefault();
            return partner;
        }

        public async Task<IEnumerable<VtuProducts>> ProductList(int serviceProviderId)
        {
            var products = _productsRepo.GetQueryable().Where(a => a.ServiceProviderId == serviceProviderId).ToList();
            return products;
        }

        public async Task<TopUpTransactionLog> GetTransactionById(int serviceproviderId, string transactionReference)
        {

            var transaction = _requestsRepo.GetQueryable().Where(a => a.serviceproviderid == serviceproviderId && a.transref == transactionReference).FirstOrDefault();
            return transaction;
        }

        public CarrierPrefix GetServiceProviderByPrefix(string prefix)
        {
            var carrier = _carrierRepo.GetQueryable().Where(a => a.Prefix == prefix).FirstOrDefault();
            return carrier;
        }

        public async Task<StockMaster> GetPartnerStockBalance(int partnerId, int serviceProviderId)
        {
            //var stockData = _stockmasterRepo.GetQueryable()
            //        .Where(a => a.PartnerId == partnerId && a.ServiceProviderId == serviceProviderId).FirstOrDefault();
            StockMaster stockObj = null;

            stockObj = _stockmasterRepo.GetQueryable(a => a.PartnerId == partnerId && a.ServiceProviderId == serviceProviderId).FirstOrDefault();
            if (stockObj == null)
            {
                StockMaster masterrecord = new StockMaster
                {
                    ServiceProviderId = serviceProviderId,
                    PartnerId = partnerId,
                    TenantId = 1,
                    QuantityOnHand = 0
                };


                _logger.LogInformation($"transactionrecord adding stock master: {JsonConvert.SerializeObject(masterrecord)}");
                await _stockmasterRepo.AddAsync(masterrecord);

            }
            return stockObj;
        }
        public async Task<bool> AddProductbyServiceProvider(List<VtuProducts> vtuProducts)
        {
            foreach (VtuProducts item in vtuProducts)
            {
                var dbentuty = _telcoprpductRepo.GetQueryable()
                    .Where((a => a.ProductId == item.ProductId && a.Price == item.Price)).FirstOrDefault();
                if(dbentuty == null)
                {
                    _logger.LogInformation($"Adding new product to catalogue : {item.ProductName}");
                    await _telcoprpductRepo.AddAsync(item);
                }
                else
                {
                    _logger.LogInformation($"Update old product to catalogue : {item.ProductId}");
                    dbentuty.Price = item.Price;
                    dbentuty.ProductId = item.ProductId;
                    dbentuty.ProductName = item.ProductName;
                    dbentuty.ProductType = item.ProductType;
                    dbentuty.ServiceProviderId = item.ServiceProviderId;
                    dbentuty.TenantUd = item.TenantUd;
                    dbentuty.Validity = item.Validity;
                    await _telcoprpductRepo.UpdateAsync(dbentuty);
                }
            }
            
            return true;
        }
        public int CarrierLookup1(string msisdn)
        {
            int serviceprovider = 0;
            try
            {
                string prefix = msisdn.Substring(0, 4);
                var carrier = GetServiceProviderByPrefix(prefix);
                return carrier.ServiceProviderId;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"failed to lookup carrier info for MSISDN : {msisdn}");
            }

            return serviceprovider;
        }
    }
}
