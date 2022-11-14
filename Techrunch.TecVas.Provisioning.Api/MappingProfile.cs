using AutoMapper;
using Techrunch.TecVas.Provisioning.Api.ViewModels;
using Techrunch.TecVas.Entities.BillPayments;
using Techrunch.TecVas.Entities.BillPayments.AbujaDisco;
using Techrunch.TecVas.Entities.BillPayments.BulkSMS;
using Techrunch.TecVas.Entities.BillPayments.Carpaddy;
using Techrunch.TecVas.Entities.BillPayments.Cornerstone;
using Techrunch.TecVas.Entities.BillPayments.EkoElectric;
using Techrunch.TecVas.Entities.BillPayments.IkejaElectric;
using Techrunch.TecVas.Entities.BillPayments.JosElectricity;
using Techrunch.TecVas.Entities.BillPayments.Kaduna;
using Techrunch.TecVas.Entities.BillPayments.Kedco;
using Techrunch.TecVas.Entities.BillPayments.Multichoice;
using Techrunch.TecVas.Entities.BillPayments.Mutual;
using Techrunch.TecVas.Entities.BillPayments.PortharcourtElectric;
using Techrunch.TecVas.Entities.BillPayments.ProxyResponses;
using Techrunch.TecVas.Entities.BillPayments.Showmax;
using Techrunch.TecVas.Entities.BillPayments.Smile;
using Techrunch.TecVas.Entities.BillPayments.Spectranet;
using Techrunch.TecVas.Entities.BillPayments.Startimes;
using Techrunch.TecVas.Entities.BillPayments.Waec;
using Techrunch.TecVas.Entities.IbadanDisco;
using Techrunch.TecVas.Services.BillPayments.Jamb;
using Techrunch.TecVas.Services.BillPayments.Proxy;
using static Techrunch.TecVas.Entities.BillPayments.AbujaDisco.AbujaPostpaidRequest;
using static Techrunch.TecVas.Entities.BillPayments.AbujaDisco.AbujaPrepaidRequest;
using static Techrunch.TecVas.Entities.BillPayments.BillpaymentRequest;
using static Techrunch.TecVas.Entities.BillPayments.BulkSMS.BulkSMSRequest;
using static Techrunch.TecVas.Entities.BillPayments.Cornerstone.CornerstoneRequest;
using static Techrunch.TecVas.Entities.BillPayments.EkoElectric.EkoElectricPostpaidRequest;
using static Techrunch.TecVas.Entities.BillPayments.EkoElectric.EkoElectricPrepaidRequest;
using static Techrunch.TecVas.Entities.BillPayments.IkejaElectric.IkejaElectricPostpaidRequest;
using static Techrunch.TecVas.Entities.BillPayments.IkejaElectric.IkejaElectricTokenPurchaseRequest;
using static Techrunch.TecVas.Entities.BillPayments.JosElectricity.JosElectricPostPaidRequest;
using static Techrunch.TecVas.Entities.BillPayments.JosElectricity.JosElectricPrePaidRequest;
using static Techrunch.TecVas.Entities.BillPayments.Kaduna.KadunaElectricPostpaidRequest;
using static Techrunch.TecVas.Entities.BillPayments.Kaduna.KadunaElectricPrepaidRequest;
using static Techrunch.TecVas.Entities.BillPayments.Kedco.KedcoElectricPostpaidRequest;
using static Techrunch.TecVas.Entities.BillPayments.Kedco.KedcoElectricPrepaidRequest;
using static Techrunch.TecVas.Entities.BillPayments.Multichoice.DstvBoxOfficeRequest;
using static Techrunch.TecVas.Entities.BillPayments.Multichoice.DstvRenewRequest;
using static Techrunch.TecVas.Entities.BillPayments.Multichoice.DstvRequest;
using static Techrunch.TecVas.Entities.BillPayments.Multichoice.GotvRenew;
using static Techrunch.TecVas.Entities.BillPayments.Multichoice.GotvRequest;
using static Techrunch.TecVas.Entities.BillPayments.Mutual.MutualMortorInsuranceRequest;
using static Techrunch.TecVas.Entities.BillPayments.PortharcourtElectric.PortHarcourtElectricPostpaidRequest;
using static Techrunch.TecVas.Entities.BillPayments.PortharcourtElectric.PortHarcourtElectricPrepaidRequest;
using static Techrunch.TecVas.Entities.BillPayments.ProxyRequest;
using static Techrunch.TecVas.Entities.BillPayments.ProxyResponse;
using static Techrunch.TecVas.Entities.BillPayments.ProxyResponses.ProxySmileResponses;
using static Techrunch.TecVas.Entities.BillPayments.ProxyResponses.ProxyStartimesResponses;
//using static Techrunch.TecVas.Entities.BillPayments.ProxyResponses.ProxyIkejaResponse;
using static Techrunch.TecVas.Entities.BillPayments.Showmax.ShowmaxVoucherRequest;
using static Techrunch.TecVas.Entities.BillPayments.Smile.SmileCommBundleRequest;
using static Techrunch.TecVas.Entities.BillPayments.Smile.SmileCommRechargeRequest;
using static Techrunch.TecVas.Entities.BillPayments.Spectranet.SpectranetPaymentPlanRequest;
using static Techrunch.TecVas.Entities.BillPayments.Spectranet.SpectranetPINRequest;
using static Techrunch.TecVas.Entities.BillPayments.Spectranet.SpectranetRefillRequest;
using static Techrunch.TecVas.Entities.BillPayments.Startimes.StartimesRequest;
using static Techrunch.TecVas.Entities.BillPayments.Waec.WaecPINRequest;
using static Techrunch.TecVas.Entities.IbadanDisco.IbadanDiscoPostpaidRequest;
using static Techrunch.TecVas.Entities.IbadanDisco.IbadanDiscoPrepaidRequest;
using static Techrunch.TecVas.Services.BillPayments.Jamb.JambPINRequest;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyAbujaPostpaid;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyAbujaPrepaid;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyCarpaddy;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyCornerSton3rdpartyInsuranceQuotation;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyCornerstone;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyCornerStoneCarModels;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyCornerStoneGetPolicyDetails;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyEkoPostpaid;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyEkoPrepaid;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyIbadanPostpaid;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyIbadanPrepaid;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyIkejaPostpaid;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyIkejaPrepaid;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyJosPostpaid;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyJosPrepaid;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyKadunaPostpaid;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyKadunaPrepaid;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyKedcoPostpaid;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyKedcoPrepaid;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyMultichoice;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyMultichoiceGetBouquet;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyMultichoiceProductAddon;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyMultichoiceValidateSmartcardNo;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyPortharcoutPostpaid;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyPortharcoutPrepaid;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxySmileGetBundles;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxySmileRecharge;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxySpectranetValidateAcctNo;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyStartimes;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyStartimesGetBundle;
using static Techrunch.TecVas.Services.BillPayments.Proxy.ProxyValidateSmileBundleCustomer;

namespace Techrunch.TecVas.Provisioning.Api
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BillpaymentRequestDetails, AbujaPostpaidRequestDetails>();
            CreateMap<BillpaymentRequest, AbujaPostpaidRequest>();

            CreateMap<BillpaymentRequestDetails, AbujaPrepaidDetails>();
            CreateMap<BillpaymentRequest, AbujaPrepaidRequest>();

            CreateMap<BillpaymentRequestDetails, BulkSMSDetails>();
            CreateMap<BillpaymentRequest, BulkSMSRequest>();

            CreateMap<BillpaymentRequest, CarpaddyRequest>();

            CreateMap<BillpaymentRequestDetails, CornerstoneRequestDetails>();
            CreateMap<BillpaymentRequest, CornerstoneRequest>();

            CreateMap<BillpaymentRequestDetails, EkoElectricPostpaidDetails>();
            CreateMap<BillpaymentRequest, EkoElectricPostpaidRequest>();

            CreateMap<BillpaymentRequestDetails, EkoElectricPrepaidRequestDetails>();
            CreateMap<BillpaymentRequest, EkoElectricPrepaidRequest>();

            CreateMap<BillpaymentRequestDetails, IbadanDiscoPostpaidRequestDetails>();
            CreateMap<BillpaymentRequest, IbadanDiscoPostpaidRequest>();

            CreateMap<BillpaymentRequestDetails, IbadanDiscoPrepaidRequestDetails>();
            CreateMap<BillpaymentRequest, IbadanDiscoPrepaidRequest>();
            //
            
            CreateMap<BillpaymentRequestDetails, IkejaElectricPostpaidDetails>();
            CreateMap<BillpaymentRequest, IkejaElectricPostpaidRequest>();

            CreateMap<BillpaymentRequestDetails, IkejaElectricTokenPurchaseDetails>();
            CreateMap<BillpaymentRequest, IkejaElectricTokenPurchaseRequest>();

            CreateMap<BillpaymentRequestDetails, JambCandidatePINDetails>();
            CreateMap<BillpaymentRequest, JambPINRequest>();

            CreateMap<BillpaymentRequestDetails, JosElectricityPrePaidDetails>();
            CreateMap<BillpaymentRequest, JosElectricPrePaidRequest>();


            CreateMap<BillpaymentRequestDetails, JosElectricityPostPaidDetails>();
            CreateMap<BillpaymentRequest, JosElectricPostPaidRequest>();


            CreateMap<BillpaymentRequestDetails, KadunaElectricPrepaidDetails>();
            CreateMap<BillpaymentRequest, KadunaElectricPrepaidRequest>();

            CreateMap<BillpaymentRequestDetails, KadunaElectricPostpaidDetails>();
            CreateMap<BillpaymentRequest, KadunaElectricPostpaidRequest>();
            //
            CreateMap<BillpaymentRequestDetails, KedcoElectricityPrepaidDetails>();
            CreateMap<BillpaymentRequest, KedcoElectricPrepaidRequest>();


            CreateMap<BillpaymentRequestDetails, KedcoElectricityPostpaidDetails>();
            CreateMap<BillpaymentRequest, KedcoElectricPostpaidRequest>();



            CreateMap<BillpaymentRequestDetails, BoxOfficerequestDetails>()
                .ForMember(a => a.amount, opt => opt.MapFrom(src => (int)src.amount));
            CreateMap<BillpaymentRequestDetails, DstvRequestDetails>()
                .ForMember(a => a.amount, opt => opt.MapFrom(src => (int)src.amount)); 
            CreateMap<BillpaymentRequestDetails, DstvRenewRequestDetails>()
                .ForMember(a => a.amount, opt => opt.MapFrom(src => (int)src.amount));

            CreateMap<BillpaymentRequest, DstvRequest>();
            CreateMap<BillpaymentRequest, DstvRenewRequest>();
            CreateMap<BillpaymentRequest, DstvBoxOfficeRequest>();
            
            CreateMap<BillpaymentRequestDetails, GotvRequestDetails>(); 
            CreateMap<BillpaymentRequest, GotvRequest>();

            CreateMap<BillpaymentRequestDetails, GotvRenewDetails>();
            CreateMap<BillpaymentRequest, GotvRenew>();


            CreateMap<BillpaymentRequestDetails, MutualMortorInsurancDetails>();
            CreateMap<BillpaymentRequest, MutualMortorInsuranceRequest>();

            CreateMap<BillpaymentRequestDetails, PortHarcourtElectricPrepaidDetails>();
            CreateMap<BillpaymentRequest, PortHarcourtElectricPrepaidRequest>();

            CreateMap<BillpaymentRequestDetails, PortHarcourtElectricPostpaidDetails>();
            CreateMap<BillpaymentRequest, PortHarcourtElectricPostpaidRequest>();


            CreateMap<BillpaymentRequestDetails, ShowmaxVoucherDetails>();
            CreateMap<BillpaymentRequest, ShowmaxVoucherRequest>();

            CreateMap<BillpaymentRequestDetails, SmileCommBundleDetails>();
            CreateMap<BillpaymentRequest, SmileCommBundleRequest>();


            //SpectranetPaymentPlanRequest
            CreateMap<BillpaymentRequestDetails, SmileRechargeDetails>();
            CreateMap<BillpaymentRequest, SmileCommRechargeRequest>();

            CreateMap<BillpaymentRequestDetails, SpectranetPaymentPlanDetails>(); 
            CreateMap<BillpaymentRequest, SpectranetPaymentPlanRequest>();

            CreateMap<BillpaymentRequestDetails, SpectranetPINDetails>(); 
            CreateMap<BillpaymentRequest, SpectranetPINRequest>();

            CreateMap<BillpaymentRequestDetails, SpectranetRefillDetails>();
            CreateMap<BillpaymentRequest, SpectranetRefillRequest>(); 

            CreateMap<BillpaymentRequestDetails, StartTimesDetails>();
            CreateMap<BillpaymentRequest, StartimesRequest>();

            CreateMap<BillpaymentRequestDetails, WaecPINDetails>();
            CreateMap<BillpaymentRequest, WaecPINRequest>();

            //.ForMember(a=>a.details.amount, opt => opt.MapFrom(src => (int)src.details.amount));




            ///Proxy Mappings

            CreateMap<ProxyRequestDetails, AbujaPostpaidProxyDetails>();
            CreateMap<ProxyRequest, ProxyAbujaPostpaid>();

            CreateMap<ProxyRequestDetails, AbujaPrepaidProxyDetails>();
            CreateMap<ProxyRequest, ProxyAbujaPrepaid>();

            CreateMap<ProxyRequestDetails, SmileBundleProxyDetails>();
            CreateMap<ProxyRequest, ProxyValidateSmileBundleCustomer>();

            CreateMap<ProxyRequestDetails, SmileGetBundlesProxyDetails>();
            CreateMap<ProxyRequest, ProxySmileGetBundles>();

            CreateMap<ProxyRequestDetails, StartimesProxyDetails>();
            CreateMap<ProxyRequest, ProxyStartimes>();

            CreateMap<ProxyRequestDetails, StartimesGetBundleProxyDetails>();
            CreateMap<ProxyRequest, ProxyStartimesGetBundle>();

            CreateMap<ProxyRequestDetails, CornerStoneCarModelsProxyDetails>();
            CreateMap<ProxyRequest, ProxyCornerStoneCarModels>();

            CreateMap<ProxyRequestDetails, CornerSton3rdpartyInsuranceQuotationProxyDetails>();
            CreateMap<ProxyRequest, ProxyCornerSton3rdpartyInsuranceQuotation>();

            CreateMap<ProxyRequestDetails, GetPolicyDetailsProxyDetails>();
            CreateMap<ProxyRequest, ProxyCornerStoneGetPolicyDetails>();

            CreateMap<ProxyRequestDetails, MultichoiceValidateSmartcardNoProxyDetails>();
            CreateMap<ProxyRequest, ProxyMultichoiceValidateSmartcardNo>();

            CreateMap<ProxyRequestDetails, MultichoiceGetBouquetProxyDetails>();
            CreateMap<ProxyRequest, ProxyMultichoiceGetBouquet>();
            CreateMap<ProxyRequestDetails, FindStandaloneProductsProxyDetails>();
            CreateMap<ProxyRequest, ProxyMultichoice>();

            CreateMap<ProxyRequestDetails, MultichoiceProductAddonProxyDetails>();
            CreateMap<ProxyRequest, ProxyMultichoiceProductAddon>();

            CreateMap<ProxyRequestDetails, ProxyPortharcourtPrepaidDetails>();
            CreateMap<ProxyRequest, ProxyPortharcoutPrepaid>();

            CreateMap<ProxyRequestDetails, PortharcoutPostpaidProxyDetails>();
            CreateMap<ProxyRequest, ProxyPortharcoutPostpaid>();

            CreateMap<ProxyRequestDetails, EkoPostpaidProxyDetails>();
            CreateMap<ProxyRequest, ProxyEkoPostpaid>();

            CreateMap<ProxyRequestDetails, EkoPrepaidProxyDetails>();
            CreateMap<ProxyRequest, ProxyEkoPrepaid>();

            CreateMap<ProxyRequestDetails, JosPrepaidProxyDetails>();
            CreateMap<ProxyRequest, ProxyJosPrepaid>();

            CreateMap<ProxyRequestDetails, JosPostpaidProxyDetails>();
            CreateMap<ProxyRequest, ProxyJosPostpaid>();

            CreateMap<ProxyRequestDetails, IkejaPostpaidProxyDetails>();
            CreateMap<ProxyRequest, ProxyIkejaPostpaid>();

            CreateMap<ProxyRequestDetails, IkejaPrepaidProxyDetails>();
            CreateMap<ProxyRequest, ProxyIkejaPrepaid>();

            CreateMap<ProxyRequestDetails, IbadanPrepaidProxyDetails>();
            CreateMap<ProxyRequest, ProxyIbadanPrepaid>();

            CreateMap<ProxyRequestDetails, IbadanPostpaidProxyDetails>();
            CreateMap<ProxyRequest, ProxyIbadanPostpaid>();

            CreateMap<ProxyRequestDetails, KedcoPrepaidProxyDetails>();
            CreateMap<ProxyRequest, ProxyKedcoPrepaid>();

            CreateMap<ProxyRequestDetails, KedcoPostpaidProxyDetails>();
            CreateMap<ProxyRequest, ProxyKedcoPostpaid>();

            CreateMap<ProxyRequestDetails, KadunaPrepaidProxyDetails>();
            CreateMap<ProxyRequest, ProxyKadunaPrepaid>();

            CreateMap<ProxyRequestDetails, KadunaPostpaidProxyDetails>();
            CreateMap<ProxyRequest, ProxyKadunaPostpaid>();

            
            CreateMap<ProxyRequest, ProxyWAECPIN>();
            CreateMap<ProxyRequest, ProxyBulkSMS>();
            CreateMap<ProxyRequest, ProxySpectranetPIN>();

            CreateMap<ProxyRequestDetails, SmileRechargeProxyDetails>();
            CreateMap<ProxyRequest, ProxySmileRecharge>();


            CreateMap<ProxyRequestDetails, SpectranetValidateProxyDetails>();
            CreateMap<ProxyRequest, ProxySpectranetValidateAcctNo>();

            CreateMap<ProxyRequestDetails, CarpaddyProxyDetails>();
            CreateMap<ProxyRequest, ProxyCarpaddy>();

            CreateMap<ProxyRequest, ProxyShowmaxVouchers>();



            ////
            ///Response 
            ///

            CreateMap<ProxyResponseDetails, ProxyIkejaResponseDetails>();
            CreateMap<ProxyResponseDetails, ProxySmileValidateCustomerDetails>();
            CreateMap<ProxyResponseDetails.Bundle, ProxySmileResponses.Bundle>();
            CreateMap<ProxyResponseDetails, ProxySmileGetbundlesDetails>();
            CreateMap<ProxyResponseDetails, ProxyStartimesValidateCustomerDetails>();

            CreateMap<ProxyResponse.Item, ProxyStartimesResponses.Item>();
            CreateMap< ProxyResponse.Bouquets, ProxyStartimesResponses.Bouquets>();            
            CreateMap<ProxyResponse, ProxyStartimesResponses>();



            //CreateMap<ProxyResponse, ProxyIkejaResponse>();

        }
    }
}
