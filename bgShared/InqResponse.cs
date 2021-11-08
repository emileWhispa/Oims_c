using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bMobile.Shared
{
    public class InqResponse
    {
        public InqRequest Request { get; set; }
        public ResposeTypes ResponseType { get; set; }
        public int ErrorNo { get; set; }
        public string Message { get; set; }
        public AccountBalance AccountBalanceDetails { get; set; }
        public Account AccountDetails { get; set; }
        public FxRate FxRate { get; set; }
        public StatusDetail StatusDetail { get; set; }
    }
   
    public class Account
    {
        public string AccountNo { get; set; }
        public string AccountNo2 { get; set; }
        public string CustomerNo { get; set; }
        public string AccountName { get; set; }
        public string Branch { get; set; }
        public string Currency { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string AccountStatus { get; set; }
        public string AccountStatusFrozen { get; set; }
        public string AccountStatusDormant { get; set; }
        public string AccountStatusStopPay { get; set; }
        public string AccountStatusNoCR { get; set; }
        public string AccountStatusNoDR { get; set; }
        public List<AccountMandates> Mandates { get; set; }

        public Account()
        {
            this.Mandates = new List<AccountMandates>();
        }
    }

    public class AccountMandates
    {
        public string Mandate { get; set; }
        public string SignitoryName { get; set; }
        public string SignitoryAddress { get; set; }
        public byte[] Signature { get; set; }
    }

    public class AccountBalance
    {
        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        public string AccountName { get; set; }
        public string Branch { get; set; }
        public string Currency { get; set; }
        public string DC { get; set; }
        public decimal ActualBalance { get; set; }
        public decimal AvailableBalance { get; set; }
    }

    public class FxRate
    {
        public decimal Rate { get; set; }
        public string Currency { get; set; }
    }
   
    public class StatusDetail
    {
        public string StatusMessage { get; set; }
        public int StatusCode { get; set; }
    }

    public enum ResposeTypes { OK, ERROR }
}
