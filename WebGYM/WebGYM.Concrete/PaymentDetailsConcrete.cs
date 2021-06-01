using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebGYM.Interface;
using WebGYM.Models;
using WebGYM.ViewModels;
using System.Linq.Dynamic.Core;
using Dapper;

namespace WebGYM.Concrete
{
    public class PaymentDetailsConcrete : IPaymentDetails
    {
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _context;

        public PaymentDetailsConcrete(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }
        public List<PaymentDetailsViewModel> GetAll(QueryParameters queryParameters, string userId)
        {
            List<PaymentDetailsViewModel> expenseViewModels = new List<PaymentDetailsViewModel>();
            var paymentparam = userId.Split('_').ToList();
            var allItems = (from payment in _context.PaymentDetails
                            where payment.CustomerNumber == paymentparam[0]
                            & payment.PlotNumber == paymentparam[1]
                            select payment).ToList().
                            Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                            .Take(queryParameters.PageCount).ToList();
            foreach (var item in allItems)
            {
                expenseViewModels.Add(new PaymentDetailsViewModel
                {
                    CustomerName = item.CustomerName,
                    CustomerNumber=item.CustomerNumber,
                    PaymentNumber=item.PaymentNumber,
                    PlotNumber=item.PlotNumber,
                    PaymentDate=item.PaymentDate,
                    ReceivedAmount=item.ReceivedAmount,
                    CreateDate=item.CreateDate,
                    ModifyDate=item.ModifyDate
                }) ;
            }            
            return expenseViewModels;            
        }

        public int Count(string paymentkey)
        {
            var paymentparam = paymentkey.Split('_').ToList();
            var paycount = (from payment in _context.PaymentDetails
                            where payment.CustomerNumber.Equals(paymentparam[0]) && 
                            payment.PlotNumber.Equals(paymentparam[1])
                            select payment).Count();
            return paycount;
        }

        public int CountExpense(string key)
         {
        var paycount = (from payment in _context.Expenses
                        select payment).Count();
            return paycount;
        }
    public bool RenewalPayment(RenewalViewModel renewalViewModel)
        {
            using (var con = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
            {
                con.Open();
                var sqlTransaction = con.BeginTransaction();
                var paramater = new DynamicParameters();
                paramater.Add("@PaymentID", 0);
                paramater.Add("@PlanID", renewalViewModel.PlanID);
                paramater.Add("@WorkouttypeID", renewalViewModel.SchemeID);
                paramater.Add("@Paymenttype", "Cash");
                paramater.Add("@PaymentFromdt", renewalViewModel.NextRenwalDate);
                paramater.Add("@PaymentAmount", renewalViewModel.Amount);
                paramater.Add("@CreateUserID", renewalViewModel.Createdby);
                paramater.Add("@ModifyUserID", renewalViewModel.Createdby);
                paramater.Add("@RecStatus", "A");
                paramater.Add("@MemberID", renewalViewModel.MemberId);
                paramater.Add("@PaymentIDOUT", dbType: DbType.Int32, direction: ParameterDirection.Output);
                int resultPayment = con.Execute("sprocPaymentDetailsInsertUpdateSingleItem", paramater, sqlTransaction,
                    0, CommandType.StoredProcedure);

                if (resultPayment > 0)
                {
                    sqlTransaction.Commit();
                    int paymentId = paramater.Get<int>("PaymentIDOUT");
                    return true;
                }
                else
                {
                    sqlTransaction.Rollback();
                    return false;
                }
            }
        }


        public List<ExpenseViewModel> GetAllExpense(QueryParameters queryParameters, int userid)
        {
            List<ExpenseViewModel> expenseViewModels = new List<ExpenseViewModel>();
            try
            {
                var allItems = (from payment in _context.Expenses
                                                              select payment);


                var allitem= allItems
                    .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                    .Take(queryParameters.PageCount).ToList();
                foreach(var item in allitem)
                {
                    var expenseitem = new ExpenseViewModel();
                    expenseitem.AccountHead = item.AccountHead;
                    expenseitem.ID = item.Id;
                    expenseitem.Description = item.Description;
                    expenseitem.Amount = item.Amount;
                    expenseitem.PaymentDate = item.PaymentDate;
                    expenseitem.CreateDate = item.CreateDate;
                    expenseitem.ModifyDate = item.ModifyDate;

                    expenseViewModels.Add(expenseitem);
                }
                return expenseViewModels;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public int InsertExpense(Expense payment)
        {
            using (var con = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
            {
                con.Open();
                var sqlTransaction = con.BeginTransaction();
                var para = new DynamicParameters();
                para.Add("@AccountHead", payment.AccountHead);
                para.Add("@Description", payment.Description);
                para.Add("@Amount", payment.Amount);
                para.Add("@PaymentDate", payment.PaymentDate);
                para.Add("@CreateDate", DateTime.Now);
                para.Add("@ModifyDate", DateTime.Now);
                para.Add("@check", dbType: DbType.Int32, direction: ParameterDirection.Output);
                int resultMember = con.Execute("sp_InsertExpense", para, sqlTransaction, 0, CommandType.StoredProcedure);
                int PlotId = para.Get<int>("check");
                if (PlotId > 0)
                {
                    sqlTransaction.Commit();
                    return PlotId;
                }
                else
                {
                    sqlTransaction.Rollback();
                    return 0;
                }
                return 0;
            }
        }





    }
}
