using HootIncomingFA.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HootIncomingFA
{
    public class OutputConstructor
    {
        public async Task<string> BuildOutHoot(HootBasics hoot, ILogger log)
        {
            log.LogInformation($"Starting BuildOutHoot");

            var mactiveDataContract = new MactiveContract();
            var billingMessage = new BillingMessage();
            var adbaseData = new AdbaseData();
            var adbaseInfo = new AdBaseInfo();
            var orderCustomers = new OrderCustomers();
            var primaryCustomer = new Customer();
            var primaryPayor = new Customer();
            var payor = new MasterCustomer();
            var orderer = new MasterCustomer();
            var orderCustomerList = new List<MasterCustomer>();

            mactiveDataContract.ApiKey = "Adv@nceL0calRock$!3968u#235f4@WB";
            mactiveDataContract.OutputEnvironment = "Hoot" + hoot.Environment;
            billingMessage.OppId = "";
            billingMessage.SoId = "";
            //billingMessage.BilId = Guid.NewGuid().ToString();
            //billingMessage.BiliId = hoot.Id;
            billingMessage.BilId = hoot.Orderer;
            billingMessage.BiliId = hoot.Orderer + hoot.BillDate;
            billingMessage.SplitBill = "No";

            primaryPayor.AccountNumber = hoot.Payor;
            payor.IsPrimaryPayor = "true";
            payor.Customer = primaryPayor;

            primaryCustomer.AccountNumber = hoot.Orderer;
            orderer.IsPrimaryOrderer = "true";
            orderer.Customer = primaryCustomer;

            orderCustomerList.Add(orderer);
            orderCustomerList.Add(payor);
            orderCustomers.OrderCustomer = orderCustomerList;

            adbaseInfo.OrderCustomers = orderCustomers;
            adbaseInfo.PurchaseOrder = string.Empty;
            adbaseInfo.Package = string.Empty;
            adbaseInfo.Notes = "Information about Hoot Order here";
            adbaseInfo.AdSoldBy = hoot.SoldBy;
            adbaseInfo.AdOrderTaker = "SFGood";
            adbaseInfo.InvoiceText = "Another place to put Hoot order info";
            adbaseInfo.PaginationCode = "N";
            adbaseInfo.Company = "ZeroSum";
            adbaseInfo.AdbaseUser = hoot.SoldBy;
            adbaseInfo.BillingFlag = "Y";

            log.LogInformation($"Video = {hoot.VideoTemplatesSubTotal}");
            var adList = new List<Ad>();
            List<string> hootads = new List<string>();
            if (hoot.MonthlySubTotal != "$0.00")
            {
                hootads.Add(hoot.Orderer + "-" + hoot.BillDate + "-Mon");
            }
            if (hoot.SetupSubTotal != "$0.00")
            {
                hootads.Add(hoot.Orderer + "-" + hoot.BillDate + "-Set");
            }
            if (hoot.VideoTemplatesSubTotal != "$0.00")
            {
                hootads.Add(hoot.Orderer + "-" + hoot.BillDate + "-Vid");
            }
            billingMessage.SfReturnIds = new string[1];
            billingMessage.SfReturnIds[0] = hoot.Id;

            foreach (var buildAd in hootads)
            {
                var ad = new Ad();
                var specialPrice = new SpecialPrice();
                var specialPriceList = new List<SpecialPrice>();
                var adLocInfo = new AdLocInfo();
                var adLocInfoList = new List<AdLocInfo>();
                var runDates = new RunDates();
                var adRunDate = new AdRunDate();

                string useProduct;
                string usePlacement;
                string usePrice;

                if (buildAd.Contains("-Vid"))
                {
                    useProduct = "0S_Hoot Video Ad";
                    usePlacement = "0S Hoot Template";
                    usePrice = hoot.VideoTemplatesSubTotal;
                }
                else if (buildAd.Contains("-Set"))
                {
                    useProduct = "0S_Hoot Inv Mon Fee";
                    usePlacement = "0S Hoot Fee Setup";
                    usePrice = hoot.SetupSubTotal;
                }
                else
                {
                    useProduct = "0S_Hoot Inv Mon Fee";
                    usePlacement = "0S Hoot Fee Monthly";
                    usePrice = hoot.MonthlySubTotal;
                }

                ad.BuyerAdId = buildAd;
                ad.AdType = "ONL_Service";
                ad.AdUserUnit = "Digital";
                ad.AdWidth = string.Empty;
                ad.AdHeight = string.Empty;
                ad.AdUnitOfMeasure = "lines";
                ad.ProductionMethod = "AdBooker";
                adLocInfo.Publication = useProduct;
                adLocInfo.PublicationPlacement = usePlacement;
                adLocInfo.PublicationPosition = string.Empty;
                adRunDate.Date = hoot.BillDate;
                runDates.Date = new[] { adRunDate.Date };
                adLocInfo.RunDates = runDates;
                specialPrice.Type = "Premium";
                specialPrice.Name = "Flat Rate Premium";
                specialPrice.Value = usePrice.Replace("$", "").Replace(",", "");
                specialPriceList.Add(specialPrice);
                adLocInfo.SpecialPrice = specialPriceList;
                adLocInfoList.Add(adLocInfo);
                ad.AdLocInfo = adLocInfoList;
                adList.Add(ad);
                adbaseInfo.Ad = adList;
            }
            adbaseData.AdBaseInfo = adbaseInfo;
            billingMessage.AdbaseData = adbaseData;
            mactiveDataContract.BillingMessage = billingMessage;

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IgnoreNullValues = true
            };
            var jsonPayload = JsonSerializer.Serialize<MactiveContract>(mactiveDataContract, options);

            return jsonPayload;
        }
    }
}
