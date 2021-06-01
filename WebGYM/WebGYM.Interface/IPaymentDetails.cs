using System.Collections.Generic;
using System.Linq;
using WebGYM.Models;
using WebGYM.ViewModels;

namespace WebGYM.Interface
{
    public interface IPaymentDetails
    {
        List<PaymentDetailsViewModel> GetAll(QueryParameters queryParameters, string userId);
        int Count(string paymentkey);
        int CountExpense(string key);
        bool RenewalPayment(RenewalViewModel renewalViewModel);
        int InsertExpense(Expense payment);
        List<ExpenseViewModel> GetAllExpense(QueryParameters queryParameters, int userid);
        
    }
}