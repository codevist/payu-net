using Payu.Core;
using Payu.Core.Request;
using Payu.Core.Response;
using Payu.WebSamples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Payu.WebSamples.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {

           return View();
        }

        /// <summary>
        /// 3d secure olmadan ödeme çağrısının yapıldığı kısımdır.
        /// </summary>
        /// <param name="nameSurname"></param>
        /// <param name="cardNumber"></param>
        /// <param name="cvc"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="installment"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string nameSurname, string cardNumber, string cvc, string month, string year,string installment)
        {
            ApiPaymentRequest apiPaymentRequest = new ApiPaymentRequest();

            #region Genel Bilgiler
            apiPaymentRequest.Config = new ApiPaymentRequest.PayUConfig();
            apiPaymentRequest.Config.MERCHANT = "OPU_TEST";
            apiPaymentRequest.Config.LANGUAGE = "TR";
            apiPaymentRequest.Config.PAY_METHOD = "CCVISAMC";
            apiPaymentRequest.Config.BACK_REF = "";
            apiPaymentRequest.Config.PRICES_CURRENCY = "TRY";
            apiPaymentRequest.Order = new ApiPaymentRequest.PayUOrder();
            apiPaymentRequest.Order.ORDER_REF = Guid.NewGuid().ToString();
            apiPaymentRequest.Order.ORDER_SHIPPING = "5";
            apiPaymentRequest.Order.ORDER_DATE = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            #endregion

            #region Urun Bilgileri 
            ApiPaymentRequest.PayUOrder.PayUOrderItem orderItem = new ApiPaymentRequest.PayUOrder.PayUOrderItem();
            orderItem.ORDER_PRICE = "5";
            orderItem.ORDER_PINFO = "Test Açıklaması";
            orderItem.ORDER_QTY = "1";
            orderItem.ORDER_PCODE = "Test Kodu";
            orderItem.ORDER_PNAME = "Test Ürünü";
            orderItem.ORDER_VAT = "18";
            orderItem.ORDER_PRICE_TYPE = "NET";

            apiPaymentRequest.Order.OrderItems.Add(orderItem);
            #endregion

            #region Kredi Kartı Bilgileri
            apiPaymentRequest.CreditCard = new ApiPaymentRequest.PayUCreditCard();
            apiPaymentRequest.CreditCard.CC_NUMBER = cardNumber;
            apiPaymentRequest.CreditCard.EXP_MONTH = month;
            apiPaymentRequest.CreditCard.EXP_YEAR = year;
            apiPaymentRequest.CreditCard.CC_CVV = cvc;
            apiPaymentRequest.CreditCard.CC_OWNER = nameSurname;
            apiPaymentRequest.CreditCard.SELECTED_INSTALLMENTS_NUMBER = installment;
            #endregion

            #region Fatura Bilgileri  
            apiPaymentRequest.Customer = new ApiPaymentRequest.PayUCustomer();
            apiPaymentRequest.Customer.BILL_FNAME = "Ad";
            apiPaymentRequest.Customer.BILL_LNAME = "Soyad";
            apiPaymentRequest.Customer.BILL_EMAIL = "mail@mail.com";
            apiPaymentRequest.Customer.BILL_PHONE = "02129003711";
            apiPaymentRequest.Customer.BILL_FAX = "02129003711";
            apiPaymentRequest.Customer.BILL_ADDRESS = "Birinci Adres satırı";
            apiPaymentRequest.Customer.BILL_ADDRESS2 = "İkinci Adres satırı";
            apiPaymentRequest.Customer.BILL_ZIPCODE = "34000";
            apiPaymentRequest.Customer.BILL_CITY = "ISTANBUL";
            apiPaymentRequest.Customer.BILL_COUNTRYCODE = "TR";
            apiPaymentRequest.Customer.BILL_STATE = "Ayazağa";
            apiPaymentRequest.Customer.CLIENT_IP = Request.UserHostAddress;
            #endregion

            #region Teslimat Parametreleri
            apiPaymentRequest.Delivery = new ApiPaymentRequest.PayUDelivery();
            apiPaymentRequest.Delivery.DELIVERY_FNAME = "Ad";
            apiPaymentRequest.Delivery.DELIVERY_LNAME = "Soyad";
            apiPaymentRequest.Delivery.DELIVERY_EMAIL = "mail@mail.com";
            apiPaymentRequest.Delivery.DELIVERY_PHONE = "02129003711";
            apiPaymentRequest.Delivery.DELIVERY_COMPANY = "PayU Ödeme Kuruluşu A.Ş.";
            apiPaymentRequest.Delivery.DELIVERY_ADDRESS = "Birinci Adres satırı";
            apiPaymentRequest.Delivery.DELIVERY_ADDRESS2 = "İkinci Adres satırı";
            apiPaymentRequest.Delivery.DELIVERY_ZIPCODE = "34000";
            apiPaymentRequest.Delivery.DELIVERY_CITY = "ISTANBUL";
            apiPaymentRequest.Delivery.DELIVERY_STATE = "TR";
            apiPaymentRequest.Delivery.DELIVERY_COUNTRYCODE = "Ayazağa";
            #endregion

            var options = new Options();
            options.Url = "https://secure.payu.com.tr/order/alu/v3";
            options.SecretKey = "SECRET_KEY";
            var response = ApiPaymentRequest.Non3DExecute(apiPaymentRequest, options); //api çağrısının başlatıldığı kısmı temsil eder.
            return View(response);
        }
        public ActionResult ApiPayment3DSecure()
        {
           
            return View();
        }

        /// <summary>
        /// 3D secure ile ödeme çağrısının yapıldığı kısımdır
        /// </summary>
        /// <param name="nameSurname"></param>
        /// <param name="cardNumber"></param>
        /// <param name="cvc"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="installment"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ApiPayment3DSecure(string nameSurname, string cardNumber, string cvc, string month, string year, string installment)
        {

            var siteUrl = Request.Url.GetLeftPart(UriPartial.Authority);

            ApiPaymentRequest apiPaymentRequest = new ApiPaymentRequest();

            #region Genel Bilgiler
            apiPaymentRequest.Config = new ApiPaymentRequest.PayUConfig();
            apiPaymentRequest.Config.MERCHANT = "PALJZXGV";
            apiPaymentRequest.Config.LANGUAGE = "TR";
            apiPaymentRequest.Config.PAY_METHOD = "CCVISAMC";
            apiPaymentRequest.Config.BACK_REF = siteUrl + "/Home/ThreeDSecureBackRefPage";
            apiPaymentRequest.Config.PRICES_CURRENCY = "TRY";
            apiPaymentRequest.Order = new ApiPaymentRequest.PayUOrder();
            apiPaymentRequest.Order.ORDER_REF = Guid.NewGuid().ToString();
            apiPaymentRequest.Order.ORDER_SHIPPING = "5";
            apiPaymentRequest.Order.ORDER_DATE = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            #endregion

            #region Urun Bilgileri 
            ApiPaymentRequest.PayUOrder.PayUOrderItem orderItem = new ApiPaymentRequest.PayUOrder.PayUOrderItem();
            orderItem.ORDER_PRICE = "5";
            orderItem.ORDER_PINFO = "Test Açıklaması";
            orderItem.ORDER_QTY = "1";
            orderItem.ORDER_PCODE = "Test Kodu";
            orderItem.ORDER_PNAME = "Test Ürünü";
            orderItem.ORDER_VAT = "18";
            orderItem.ORDER_PRICE_TYPE = "NET";



            apiPaymentRequest.Order.OrderItems.Add(orderItem);

            #endregion

            #region Kredi Kartı Bilgileri
            apiPaymentRequest.CreditCard = new ApiPaymentRequest.PayUCreditCard();
            apiPaymentRequest.CreditCard.CC_NUMBER = cardNumber;
            apiPaymentRequest.CreditCard.EXP_MONTH = month;
            apiPaymentRequest.CreditCard.EXP_YEAR = year;
            apiPaymentRequest.CreditCard.CC_CVV = cvc;
            apiPaymentRequest.CreditCard.CC_OWNER = nameSurname;
            apiPaymentRequest.CreditCard.SELECTED_INSTALLMENTS_NUMBER = installment;
            #endregion

            #region Fatura Bilgileri  
            apiPaymentRequest.Customer = new ApiPaymentRequest.PayUCustomer();
            apiPaymentRequest.Customer.BILL_FNAME = "Ad";
            apiPaymentRequest.Customer.BILL_LNAME = "Soyad";
            apiPaymentRequest.Customer.BILL_EMAIL = "mail@mail.com";
            apiPaymentRequest.Customer.BILL_PHONE = "02129003711";
            apiPaymentRequest.Customer.BILL_FAX = "02129003711";
            apiPaymentRequest.Customer.BILL_ADDRESS = "Birinci Adres satırı";
            apiPaymentRequest.Customer.BILL_ADDRESS2 = "İkinci Adres satırı";
            apiPaymentRequest.Customer.BILL_ZIPCODE = "34000";
            apiPaymentRequest.Customer.BILL_CITY = "ISTANBUL";
            apiPaymentRequest.Customer.BILL_COUNTRYCODE = "TR";
            apiPaymentRequest.Customer.BILL_STATE = "Ayazağa";
            apiPaymentRequest.Customer.CLIENT_IP = Request.UserHostAddress;
            #endregion

            #region Teslimat Parametreleri
            apiPaymentRequest.Delivery = new ApiPaymentRequest.PayUDelivery();
            apiPaymentRequest.Delivery.DELIVERY_FNAME = "Ad";
            apiPaymentRequest.Delivery.DELIVERY_LNAME = "Soyad";
            apiPaymentRequest.Delivery.DELIVERY_EMAIL = "mail@mail.com";
            apiPaymentRequest.Delivery.DELIVERY_PHONE = "02129003711";
            apiPaymentRequest.Delivery.DELIVERY_COMPANY = "PayU Ödeme Kuruluşu A.Ş.";
            apiPaymentRequest.Delivery.DELIVERY_ADDRESS = "Birinci Adres satırı";
            apiPaymentRequest.Delivery.DELIVERY_ADDRESS2 = "İkinci Adres satırı";
            apiPaymentRequest.Delivery.DELIVERY_ZIPCODE = "34000";
            apiPaymentRequest.Delivery.DELIVERY_CITY = "ISTANBUL";
            apiPaymentRequest.Delivery.DELIVERY_STATE = "TR";
            apiPaymentRequest.Delivery.DELIVERY_COUNTRYCODE = "Ayazağa";

            #endregion
            var options = new Options();
            options.Url = "https://secure.payu.com.tr/order/alu/v3";
            options.SecretKey = "f*%J7z6_#|5]s7V4[g3]";
            var response = ApiPaymentRequest.ThreeDSecurePayment(apiPaymentRequest, options); //api çağrısının başlatıldığı kısmı temsil eder.
            return Redirect(response.URL_3DS);
        }

        public ActionResult ThreeDSecureBackRefPage()
        {
            var model= new ThreeDSecurePaymentResultModel();
            model.Status = Request.Form["STATUS"];
            model.ReturnMessage= Request.Form["RETURN_MESSAGE"];
            model.RefNo = Request.Form["REFNO"];
            model.ReturnCode = Request.Form["RETURN_CODE"];
           
           return View(model);
        }

        public ActionResult CammonPaymentPage()
        {
            return View();  
        }

        /// <summary>
        /// Ortak ödeme formu ödeme çağrısının yapıldığı kısımdır.
        /// </summary>
        /// <param name="installment"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CammonPaymentPage(string installment)
        {
            CommonPaymentRequest cammonPageRequest = new CommonPaymentRequest();

            #region Genel Bilgiler
            cammonPageRequest.Config = new CommonPaymentRequest.PayUConfig();
            cammonPageRequest.Order = new CommonPaymentRequest.PayUOrder();
            cammonPageRequest.Config.MERCHANT = "OPU_TEST";
            cammonPageRequest.Order.ORDER_REF = Guid.NewGuid().ToString();
            cammonPageRequest.Order.ORDER_DATE = DateTime.UtcNow.ToString("yyyy -MM-dd HH:mm:ss");
            #region Urun Bilgileri 
            CommonPaymentRequest.PayUOrder.PayUOrderItem orderItem = new CommonPaymentRequest.PayUOrder.PayUOrderItem();
            orderItem.ORDER_PNAME = "MacBook Air 13 inch";
            orderItem.ORDER_PCODE = "MBA13";
            orderItem.ORDER_PINFO = "Extended Warranty - 5 Years";
            orderItem.ORDER_PRICE = "2000";
            orderItem.ORDER_PRICE_TYPE = "GROSS";
            orderItem.ORDER_QTY = "1";
            orderItem.ORDER_VAT = "18";
            cammonPageRequest.Order.OrderItems.Add(orderItem);
            orderItem = new CommonPaymentRequest.PayUOrder.PayUOrderItem();
            orderItem.ORDER_PNAME = "iPhone 6S";
            orderItem.ORDER_PCODE = "IP6S";
            orderItem.ORDER_PINFO = "Test";
            orderItem.ORDER_PRICE = "2500.50";
            orderItem.ORDER_PRICE_TYPE = "NET";
            orderItem.ORDER_QTY = "1";
            orderItem.ORDER_VAT = "18";
            cammonPageRequest.Order.OrderItems.Add(orderItem);

            cammonPageRequest.Order.ORDER_SHIPPING = "50";
            cammonPageRequest.Config.PRICES_CURRENCY = "TRY";
            cammonPageRequest.Order.DISCOUNT = "10";
            cammonPageRequest.Customer.DESTINATION_CITY = "şşş";
            cammonPageRequest.Customer.DESTINATION_STATE = "şşş";
            cammonPageRequest.Customer.DESTINATION_COUNTRY = "RO";
            cammonPageRequest.Config.PAY_METHOD = "CCVISAMC";
            cammonPageRequest.Config.TESTORDER = "TRUE";
            cammonPageRequest.Config.LANGUAGE = "TR";
            #endregion

            #endregion
            #region Fatura Bilgileri  
            cammonPageRequest.Customer = new CommonPaymentRequest.PayUCustomer();
            cammonPageRequest.Customer.BILL_FNAME = "Ad";
            cammonPageRequest.Customer.BILL_LNAME = "Soyad";
            cammonPageRequest.Customer.BILL_EMAIL = "mail@mail.com";
            cammonPageRequest.Customer.BILL_PHONE = "02129003711";
            cammonPageRequest.Customer.BILL_FAX = "02129003711";
            cammonPageRequest.Customer.BILL_ADDRESS = "Birinci Adres satırı";
            cammonPageRequest.Customer.BILL_ADDRESS2 = "İkinci Adres satırı";
            cammonPageRequest.Customer.BILL_ZIPCODE = "34000";
            cammonPageRequest.Customer.BILL_CITY = "ISTANBUL";
            cammonPageRequest.Customer.BILL_COUNTRYCODE = "";
            cammonPageRequest.Customer.BILL_STATE = "Ayazağa";
            #endregion

            #region Teslimat Parametreleri
            cammonPageRequest.Delivery = new CommonPaymentRequest.PayUDelivery();
            cammonPageRequest.Delivery.DELIVERY_FNAME = "Ad";
            cammonPageRequest.Delivery.DELIVERY_LNAME = "Soyad";
            cammonPageRequest.Delivery.DELIVERY_EMAIL = "mail@mail.com";
            cammonPageRequest.Delivery.DELIVERY_PHONE = "02129003711";
            cammonPageRequest.Delivery.DELIVERY_COMPANY = "PayU Ödeme Kuruluşu A.Ş.";
            cammonPageRequest.Delivery.DELIVERY_ADDRESS = "Birinci Adres satırı";
            cammonPageRequest.Delivery.DELIVERY_ADDRESS2 = "İkinci Adres satırı";
            cammonPageRequest.Delivery.DELIVERY_ZIPCODE = "34000";
            cammonPageRequest.Delivery.DELIVERY_CITY = "ISTANBUL";
            cammonPageRequest.Delivery.DELIVERY_STATE = "Ayazağa";
            cammonPageRequest.Delivery.DELIVERY_COUNTRYCODE = "";

            #endregion

            var options = new Options();
            options.Url = "https://secure.payu.com.tr/order/lu.php";
            options.SecretKey = "SECRET_KEY";
            var form = CommonPaymentRequest.Execute(cammonPageRequest, options); //api çağrısının başlatıldığı kısmı temsil eder.
            System.Web.HttpContext.Current.Response.Write(form);
            System.Web.HttpContext.Current.Response.End();
            return View();
        }

      
        public ActionResult CreateToken()
        {
            return View();
        }
         /// <summary>
        /// Ödeme yapılırken Token oluşturulmasınının sağlandığı kısımdır.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateToken(string nameSurname, string cardNumber, string cvc, string month, string year, string installment)
        {
            ApiPaymentRequest apiPaymentRequest = new ApiPaymentRequest();

            #region Genel Bilgiler
            apiPaymentRequest.Config = new ApiPaymentRequest.PayUConfig();
            apiPaymentRequest.Config.MERCHANT = "OPU_TEST";
            apiPaymentRequest.Config.LANGUAGE = "TR";
            apiPaymentRequest.Config.PAY_METHOD = "CCVISAMC";
            apiPaymentRequest.Config.BACK_REF = "";
            apiPaymentRequest.Config.PRICES_CURRENCY = "TRY";
            apiPaymentRequest.Config.LU_ENABLE_TOKEN = "true";
            apiPaymentRequest.Order = new ApiPaymentRequest.PayUOrder();
            apiPaymentRequest.Order.ORDER_REF = Guid.NewGuid().ToString();
            apiPaymentRequest.Order.ORDER_SHIPPING = "5";
            apiPaymentRequest.Order.ORDER_DATE = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");

            #endregion

            #region Urun Bilgileri 
            ApiPaymentRequest.PayUOrder.PayUOrderItem orderItem = new ApiPaymentRequest.PayUOrder.PayUOrderItem();
            orderItem.ORDER_PRICE = "5";
            orderItem.ORDER_PINFO = "Test Açıklaması";
            orderItem.ORDER_QTY = "1";
            orderItem.ORDER_PCODE = "Test Kodu";
            orderItem.ORDER_PNAME = "Test Ürünü";
            orderItem.ORDER_VAT = "18";
            orderItem.ORDER_PRICE_TYPE = "NET";

            apiPaymentRequest.Order.OrderItems.Add(orderItem);
            #endregion

            #region Kredi Kartı Bilgileri
            apiPaymentRequest.CreditCard = new ApiPaymentRequest.PayUCreditCard();
            apiPaymentRequest.CreditCard.CC_NUMBER = cardNumber;
            apiPaymentRequest.CreditCard.EXP_MONTH = month;
            apiPaymentRequest.CreditCard.EXP_YEAR = year;
            apiPaymentRequest.CreditCard.CC_CVV = cvc;
            apiPaymentRequest.CreditCard.CC_OWNER = nameSurname;
            apiPaymentRequest.CreditCard.SELECTED_INSTALLMENTS_NUMBER = installment;
            #endregion

            #region Fatura Bilgileri  
            apiPaymentRequest.Customer = new ApiPaymentRequest.PayUCustomer();
            apiPaymentRequest.Customer.BILL_FNAME = "Ad";
            apiPaymentRequest.Customer.BILL_LNAME = "Soyad";
            apiPaymentRequest.Customer.BILL_EMAIL = "mail@mail.com";
            apiPaymentRequest.Customer.BILL_PHONE = "02129003711";
            apiPaymentRequest.Customer.BILL_FAX = "02129003711";
            apiPaymentRequest.Customer.BILL_ADDRESS = "Birinci Adres satırı";
            apiPaymentRequest.Customer.BILL_ADDRESS2 = "İkinci Adres satırı";
            apiPaymentRequest.Customer.BILL_ZIPCODE = "34000";
            apiPaymentRequest.Customer.BILL_CITY = "ISTANBUL";
            apiPaymentRequest.Customer.BILL_COUNTRYCODE = "TR";
            apiPaymentRequest.Customer.BILL_STATE = "Ayazağa";
            apiPaymentRequest.Customer.CLIENT_IP = Request.UserHostAddress;
            #endregion

            #region Teslimat Parametreleri
            apiPaymentRequest.Delivery = new ApiPaymentRequest.PayUDelivery();
            apiPaymentRequest.Delivery.DELIVERY_FNAME = "Ad";
            apiPaymentRequest.Delivery.DELIVERY_LNAME = "Soyad";
            apiPaymentRequest.Delivery.DELIVERY_EMAIL = "mail@mail.com";
            apiPaymentRequest.Delivery.DELIVERY_PHONE = "02129003711";
            apiPaymentRequest.Delivery.DELIVERY_COMPANY = "PayU Ödeme Kuruluşu A.Ş.";
            apiPaymentRequest.Delivery.DELIVERY_ADDRESS = "Birinci Adres satırı";
            apiPaymentRequest.Delivery.DELIVERY_ADDRESS2 = "İkinci Adres satırı";
            apiPaymentRequest.Delivery.DELIVERY_ZIPCODE = "34000";
            apiPaymentRequest.Delivery.DELIVERY_CITY = "ISTANBUL";
            apiPaymentRequest.Delivery.DELIVERY_STATE = "TR";
            apiPaymentRequest.Delivery.DELIVERY_COUNTRYCODE = "Ayazağa";
            #endregion

            var options = new Options();
            options.Url = "https://secure.payu.com.tr/order/alu/v3";
            options.SecretKey = "SECRET_KEY";
            var response = ApiPaymentRequest.Non3DExecute(apiPaymentRequest, options); //api çağrısının başlatıldığı kısmı temsil eder.
            return View(response);
        }
        public ActionResult TokenWithPayment()
        {
            return View();
        }

        /// <summary>
        /// Token ile ödeme servisidir.
        /// </summary>
        /// <param name="ccToken"></param>
        /// <param name="installment"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TokenWithPayment(string ccToken, string installment)
        {
            ApiPaymentRequest apiPaymentRequest = new ApiPaymentRequest();
            #region Genel Bilgiler
            apiPaymentRequest.Config = new ApiPaymentRequest.PayUConfig();
            apiPaymentRequest.Config.MERCHANT = "OPU_TEST";
            apiPaymentRequest.Config.LANGUAGE = "TR";
            apiPaymentRequest.Config.PAY_METHOD = "CCVISAMC";
            apiPaymentRequest.Config.BACK_REF = "";
            apiPaymentRequest.Config.PRICES_CURRENCY = "TRY";
            apiPaymentRequest.Config.LU_ENABLE_TOKEN = "true";
            apiPaymentRequest.Order = new ApiPaymentRequest.PayUOrder();
            apiPaymentRequest.Order.ORDER_REF = Guid.NewGuid().ToString();
            apiPaymentRequest.Order.ORDER_SHIPPING = "5";
            apiPaymentRequest.Order.ORDER_DATE = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");

            #endregion

            #region Urun Bilgileri 
            ApiPaymentRequest.PayUOrder.PayUOrderItem orderItem = new ApiPaymentRequest.PayUOrder.PayUOrderItem();
            orderItem.ORDER_PRICE = "5";
            orderItem.ORDER_PINFO = "Test Açıklaması";
            orderItem.ORDER_QTY = "1";
            orderItem.ORDER_PCODE = "Test Kodu";
            orderItem.ORDER_PNAME = "Test Ürünü";
            orderItem.ORDER_VAT = "18";
            orderItem.ORDER_PRICE_TYPE = "NET";

            apiPaymentRequest.Order.OrderItems.Add(orderItem);
            #endregion

            #region Kredi Kartı Bilgileri
            apiPaymentRequest.CreditCard = new ApiPaymentRequest.PayUCreditCard();
            apiPaymentRequest.CreditCard.CC_TOKEN = ccToken;
            apiPaymentRequest.CreditCard.CC_NUMBER = "";
            apiPaymentRequest.CreditCard.EXP_MONTH = "";
            apiPaymentRequest.CreditCard.EXP_YEAR = "";
            apiPaymentRequest.CreditCard.CC_CVV = "";
            apiPaymentRequest.CreditCard.CC_OWNER = "";
            apiPaymentRequest.CreditCard.SELECTED_INSTALLMENTS_NUMBER = installment;
            #endregion

            #region Fatura Bilgileri  
            apiPaymentRequest.Customer = new ApiPaymentRequest.PayUCustomer();
            apiPaymentRequest.Customer.BILL_FNAME = "Ad";
            apiPaymentRequest.Customer.BILL_LNAME = "Soyad";
            apiPaymentRequest.Customer.BILL_EMAIL = "mail@mail.com";
            apiPaymentRequest.Customer.BILL_PHONE = "02129003711";
            apiPaymentRequest.Customer.BILL_FAX = "02129003711";
            apiPaymentRequest.Customer.BILL_ADDRESS = "Birinci Adres satırı";
            apiPaymentRequest.Customer.BILL_ADDRESS2 = "İkinci Adres satırı";
            apiPaymentRequest.Customer.BILL_ZIPCODE = "34000";
            apiPaymentRequest.Customer.BILL_CITY = "ISTANBUL";
            apiPaymentRequest.Customer.BILL_COUNTRYCODE = "TR";
            apiPaymentRequest.Customer.BILL_STATE = "Ayazağa";
            apiPaymentRequest.Customer.CLIENT_IP = Request.UserHostAddress;
            #endregion

            #region Teslimat Parametreleri
            apiPaymentRequest.Delivery = new ApiPaymentRequest.PayUDelivery();
            apiPaymentRequest.Delivery.DELIVERY_FNAME = "Ad";
            apiPaymentRequest.Delivery.DELIVERY_LNAME = "Soyad";
            apiPaymentRequest.Delivery.DELIVERY_EMAIL = "mail@mail.com";
            apiPaymentRequest.Delivery.DELIVERY_PHONE = "02129003711";
            apiPaymentRequest.Delivery.DELIVERY_COMPANY = "PayU Ödeme Kuruluşu A.Ş.";
            apiPaymentRequest.Delivery.DELIVERY_ADDRESS = "Birinci Adres satırı";
            apiPaymentRequest.Delivery.DELIVERY_ADDRESS2 = "İkinci Adres satırı";
            apiPaymentRequest.Delivery.DELIVERY_ZIPCODE = "34000";
            apiPaymentRequest.Delivery.DELIVERY_CITY = "ISTANBUL";
            apiPaymentRequest.Delivery.DELIVERY_STATE = "TR";
            apiPaymentRequest.Delivery.DELIVERY_COUNTRYCODE = "Ayazağa";
            #endregion

            var options = new Options();
            options.Url = "https://secure.payu.com.tr/order/alu/v3";
            options.SecretKey = "SECRET_KEY";
            var response = ApiPaymentRequest.Non3DExecute(apiPaymentRequest, options); //api çağrısının başlatıldığı kısmı temsil eder.
            return View(response);
        }

        //İptal Iade Servisi
        public ActionResult IrnService()
        {
            return View();
        }

        /// <summary>
        /// İptal iade servisinin bulunduğu kısımdır.
        /// </summary>
        /// <param name="orderRef"></param>
        /// <param name="orderAmount"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IrnService(string orderRef,string orderAmount,string amount)
        {
            var model = new IRNResultModel();
            IRNRequest request = new IRNRequest();
            request.MERCHANT = "OPU_TEST";
            request.ORDER_REF = orderRef;
            request.ORDER_AMOUNT = orderAmount;
            request.ORDER_CURRENCY = "TRY";
            request.IRN_DATE = DateTime.Now.ToString("yyyy-MM-d HH:mm:ss");
            request.AMOUNT = amount;
            var options = new Options();
            options.Url = "https://secure.payu.com.tr/order/irn.php";
            options.SecretKey = "SECRET_KEY";
            var response = IRNRequest.Execute(request, options); //api çağrısının başlatıldığı kısmı temsil eder.
            model.IRNServiceResponse = response;

            return View(model);
        }

        /// <summary>
        /// işlem sorgu servisi
        /// </summary>
        /// <returns></returns>
        public ActionResult IOSService()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IOSService(string refnoExt)
        {
            var model = new IOSResponse();

            IOSRequest request = new IOSRequest();
            request.MERCHANT = "OPU_TEST";
            request.REFNOEXT = refnoExt;
            Options options = new Options();
            options.Url = "https://secure.payu.com.tr/order/ios.php";
            options.SecretKey = "SECRET_KEY";
            var response = IOSRequest.Execute(request, options); //api çağrısının başlatıldığı kısmı temsil eder.

            model = response;
            return View(model);
        }

        /// <summary>
        /// Konfirmasyon Servisi
        /// </summary>
        /// <returns></returns>
        public ActionResult ConfirmationService()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ConfirmationService(string orderRef,string orderAmount,string chargeAmount)
        {
            var model = new ConfirmationResultModel();
            ConfirmationRequest request = new ConfirmationRequest();
            request.MERCHANT = "OPU_TEST";
            request.ORDER_REF = orderRef;
            request.ORDER_AMOUNT = orderAmount;
            request.ORDER_CURRENCY = "TRY";
            request.IDN_DATE = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            request.CHARGE_AMOUNT = chargeAmount;
            Options options = new Options();
            options.Url = "https://secure.payu.com.tr/order/idn.php";
            options.SecretKey = "SECRET_KEY";
            var response = ConfirmationRequest.Execute(request, options); //konfirmasyon servisi servis çağrısı başlatıldığı kısım.

            model.Response = response;
            return View(model);
        }


        public ActionResult PointCheck()
        {
            return View();
        }

        /// <summary>
        /// Puan sorgulama servisi
        /// </summary>
        /// <param name="cvv"></param>
        /// <param name="owner"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PointCheck(string cvv,string owner,string year,string month,string cardNumber)
        {
            PointCheckRequest request = new PointCheckRequest();
            request.MERCHANT = "OPU_TEST";
            request.CC_OWNER = owner;
            request.CC_NUMBER = cardNumber;
            request.EXP_MONTH = month;
            request.EXP_YEAR = year;
            request.CC_CVV = cvv;
            request.CURRENCY = "TRY";
            request.DATE = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            Options options = new Options();
            options.Url = "https://secure.payu.com.tr/api/loyalty-points/check";
            options.SecretKey = "SECRET_KEY";
            var response = PointCheckRequest.Execute(request, options); //api çağrısının başlatıldığı kısmı temsil eder.
            return View(response);
        }
        public ActionResult PointCheckWithToken()
        {
            return View();
        }

        /// <summary>
        /// Token ile puan sorgulama
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PointCheckWithToken(string token)
        {
            PointCheckWithTokenRequest request = new PointCheckWithTokenRequest();
            request.MERCHANT = "OPU_TEST";
            request.CURRENCY = "TRY";
            request.TOKEN = token;
            request.DATE = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            Options options = new Options();
            options.Url = "https://secure.payu.com.tr/api/loyalty-points/check";
            options.SecretKey = "SECRET_KEY";
            var response = PointCheckWithTokenRequest.Execute(request, options); //api çağrısının başlatıldığı kısmı temsil eder.
            return View(response);
        }

        public ActionResult OrderReport()
        {

            return View();
        }
        /// <summary>
        /// sipariş raporları
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OrderReport(string startDate, string endDate)
        {
            var model = new ReportResponseModel();
            int unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            ReportRequest request = new ReportRequest();
            request.merchant = "OPU_TEST";
            request.startDate = startDate;
            request.endDate = endDate;
            request.timeStamp = unixTimestamp.ToString();
            Options options = new Options();
            options.Url = "https://secure.payu.com.tr/reports/orders";
            options.SecretKey = "SECRET_KEY";
            model.Response = ReportRequest.Execute(request, options); //api çağrısının başlatıldığı kısmı temsil eder.
            return View(model);
        }

        public ActionResult ProductReport()
        {
            return View();
        }

        /// <summary>
        /// Ürün raporları
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ProductReport(string startDate,string endDate)
        {
            var model = new ReportResponseModel();
            int unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            ReportRequest request = new ReportRequest();
            request.merchant = "OPU_TEST";
            request.startDate = startDate;
            request.endDate = endDate;
            request.timeStamp = unixTimestamp.ToString();
            Options options = new Options();
            options.Url = "https://secure.payu.com.tr/reports/products";
            options.SecretKey = "SECRET_KEY";

            model.Response = ReportRequest.Execute(request, options); //api çağrısının başlatıldığı kısmı temsil eder.
            return View(model);
        }

        public ActionResult BinServiceV1()
        {
            return View();
        }
        /// <summary>
        /// Bin numarasına göre işlem sorgulama
        /// </summary>
        /// <param name="binNumber"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BinServiceV1(string binNumber)
        {

            var model = new BINServicesResultModel();

            BinV1Request request = new BinV1Request();
            request.BIN = binNumber;
            request.MERCHANT = "OPU_TEST";
            request.TIMESTAMP = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Options options = new Options();
            options.Url = "https://secure.payu.com.tr/api/card-info/v1/";
            options.SecretKey = "SECRET_KEY";
            model.Response= BinV1Request.Execute(request, options); //api çağrısının başlatıldığı kısmı temsil eder.
            return View(model);
        }

        public ActionResult BinServiceV2()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BinServiceV2(string nameSurname, string cardNumber, string cvc, string month, string year)
        {
            BINServicesResultModel model = new BINServicesResultModel();
            BinV2Request request = new BinV2Request();
            request.MERCHANT = "OPU_TEST";
            request.DATETIME = DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss+00:00");
            request.EXTRAINFO = "true";
            request.CC_CVV = cvc;
            request.CC_OWNER = nameSurname;
            request.EXP_YEAR =year;
            request.EXP_MONTH = month;
            request.CC_NUMBER = cardNumber;
            Options options = new Options();
            options.Url = "https://secure.payu.com.tr/api/card-info/v2/";
            options.SecretKey = "SECRET_KEY";
            model.Response =BinV2Request.Execute(request, options);
            return View(model);
        }


        public ActionResult GetTokenInformation()
        {
            return View();
        }

        /// <summary>
        /// Token bilgileri sorgulama ekranı
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetTokenInformation(string token)
        {

            TokenResponseModel model= new TokenResponseModel();

            int timeStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            GetTokenInformationRequest request = new GetTokenInformationRequest();
            request.MERCHANT = "OPU_TEST";
            request.TIMESTAMP = timeStamp.ToString();
            request.TOKEN = token;
            Options options = new Options();
            options.Url = "https://secure.payu.com.tr/order/token/v2/merchantToken/";
            options.SecretKey = "SECRET_KEY";
            model.TokenResponse = GetTokenInformationRequest.Execute(request, options); //api çağrısının başlatıldığı kısmı temsil eder.
            return View(model);
        }

        public ActionResult TokenHistory()
        {
            return View();
        }
        /// <summary>
        /// Token geçmişi sorgulama ekranı
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TokenHistory(string token)
        {
            TokenResponseModel model = new TokenResponseModel();

            int timeStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            TokenHistoryRequest request = new TokenHistoryRequest();
            request.MERCHANT = "OPU_TEST";
            request.TIMESTAMP = timeStamp.ToString();
            request.TOKEN = token;
            Options options = new Options();
            options.Url = "https://secure.payu.com.tr/order/token/v2/merchantToken/";
            options.SecretKey = "SECRET_KEY";
            model.TokenResponse = TokenHistoryRequest.Execute(request, options); //api çağrısının başlatıldığı kısmı temsil eder.
            return View(model);
        }
        public ActionResult CancelToken()
        {

            return View();
        }
        /// <summary>
        /// Token iptal etme 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CancelToken(string token,string reason)
        {
            TokenResponseModel model = new TokenResponseModel();

            int timeStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            CancelTokenRequest request = new CancelTokenRequest();
            request.MERCHANT = "OPU_TEST";
            request.CANCELREASON = reason;
            request.TIMESTAMP = timeStamp.ToString();
            request.TOKEN = token;
            Options options = new Options();
            options.Url = "https://secure.payu.com.tr/order/token/v2/merchantToken/";
            options.SecretKey = "SECRET_KEY";
            model.TokenResponse = CancelTokenRequest.Execute(request, options); //api çağrısının başlatıldığı kısmı temsil eder.
            return View(model);
        }
        public ActionResult GetMultipleToken()
        {
            return View();
        }
        /// <summary>
        /// Çoklu token sorgulama
        /// </summary>
        /// <param name="token1"></param>
        /// <param name="token2"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMultipleToken(string token1, string token2)
        {
            TokenResponseModel model = new TokenResponseModel();

            int timeStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            MultipleTokenRequest request = new MultipleTokenRequest();
            request.TOKENS = new List<Token>();
            request.MERCHANT = "OPU_TEST";
            request.TIMESTAMP = timeStamp.ToString();
            Token t = new Token();
            t.TOKEN = token1;   
            request.TOKENS.Add(t);
            t = new Token();
            t.TOKEN = token2;
            request.TOKENS.Add(t);
            Options options = new Options();
            options.Url = "https://secure.payu.com.tr/order/token/v2/";
            options.SecretKey = "SECRET_KEY";
            model.TokenResponse = MultipleTokenRequest.Execute(request, options); //api çağrısının başlatıldığı kısmı temsil eder.
            return View(model);
        }

        public ActionResult TokenServiceCreateToken()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TokenServiceCreateToken(string refNo)
        {
            TokenResponseModel model = new TokenResponseModel();
            int timeStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            TokenServiceCreateTokenRequest request = new TokenServiceCreateTokenRequest();
            request.MERCHANT = "OPU_TEST";
            request.REFNO = refNo;
            request.TIMESTAMP = timeStamp.ToString();
            Options options = new Options();
            options.Url = "https://secure.payu.com.tr/order/token/v2/merchantToken/";
            options.SecretKey = "SECRET_KEY";

            model.TokenResponse=TokenServiceCreateTokenRequest.Execute(request,options);
            return View(model);
        }

        /// <summary>
        /// Asenkron servis çağrısı
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AsyncNotificationService()
        {
            var model = new AsyncNotificationResultModel();
            AsyncNotificationServiceHashRequest request = new AsyncNotificationServiceHashRequest();
            request.IPN_PID = Request.Form["IPN_PID[]"];
            request.IPN_PNAME = Request.Form["IPN_PNAME[]"];
            request.IPN_DATE = Request.Form["IPN_DATE[]"];
            request.DATE = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var hash = AsyncNotificationServiceHashRequest.CalculateHash(request, "SECRET_KEY");
            model.Response = string.Format("<EPAYMENT>{0}|{1}</EPAYMENT>", request.DATE, hash);
            return View(model);
        }
    }
}