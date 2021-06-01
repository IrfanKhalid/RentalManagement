using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using WebGYM.Interface;
using WebGYM.Models;
using WebGYM.ViewModels;
using System.Linq.Dynamic.Core;

namespace WebGYM.Concrete
{
    public class MemberRegistrationConcrete : IMemberRegistration
    {
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _context;


        public MemberRegistrationConcrete(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        public bool CheckNameExits(string memberFName, string memberLName, string memberMName)
        {
            var result = (from member in _context.MemberRegistration
                          where member.MemberLName == memberLName && member.MemberFName == memberFName &&
                                member.MemberMName == memberMName
                          select member.Id
                ).Count();

            return result > 0 ? true : false;
        }

        public long CheckNameExitsforUpdate(string memberFName, string memberLName, string memberMName)
        {
            var memberCount = (from member in _context.MemberRegistration
                               where member.MemberLName == memberLName && member.MemberFName == memberFName &&
                                     member.MemberMName == memberMName
                               select member.Id
                ).Count();

            if (memberCount > 0)
            {
                var result = (from member in _context.MemberRegistration
                              where member.MemberLName == memberLName && member.MemberFName == memberFName &&
                                    member.MemberMName == memberMName
                              select member.Id
                    ).FirstOrDefault();

                return result;
            }
            else
            {
                return 0;
            }
        }

        public bool DeleteMember(long memberId)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
            {
                string val = string.Empty;
                var para = new DynamicParameters();
                para.Add("@MemberId", memberId);
                int result = con.Execute("sprocMemberRegistrationDeleteSingleItem", para, null, 0, CommandType.StoredProcedure);
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public MemberRegistrationViewModel GetMemberbyId(int memberId)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
            {
                var para = new DynamicParameters();
                para.Add("@memberId", memberId);
                return con.Query<MemberRegistrationViewModel>("sp_ReadMember", para, null, true, 0, commandType: CommandType.StoredProcedure).Single();
            }
        }


        public List<MemberRegistrationGridModel> GetMemberList()
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
            {
                return con.Query<MemberRegistrationGridModel>("sprocMemberRegistrationSelectList", null, null, true, 0, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        

        public int InsertAttachPlot(AttahPlot plot)
        {
            using (var con = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
            {
                con.Open();
                var sqlTransaction = con.BeginTransaction();
                var para = new DynamicParameters();     
                para.Add("@plotno", plot.PlotNo);
                para.Add("@PlotArea", plot.PlotArea);
                para.Add("@ratepermerla", plot.RateperMerla);
                para.Add("@pricetobedecided", plot.PriceToBeDecided);
                para.Add("@totalamount", plot.TotalAmount);
                para.Add("@bookingdate", plot.BookingDate);
                para.Add("@bookingauthority", plot.BookingAuthority);
                para.Add("@timelimit", plot.TimeLimit);
                para.Add("@createddate", DateTime.Now);
                para.Add("@check", dbType: DbType.Int32, direction: ParameterDirection.Output);
                int resultMember = con.Execute("sp_InsertPlot", para, sqlTransaction, 0, CommandType.StoredProcedure);
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

        public int InsertPayment(PaymentDetails payment)
        {
            using (var con = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
            {
                con.Open();
                var sqlTransaction = con.BeginTransaction();
                var para = new DynamicParameters();
                para.Add("@CustomerName", payment.CustomerName);
                para.Add("@CustomerNumber", payment.CustomerNumber);
                para.Add("@PaymentDate", payment.PaymentDate);
                para.Add("@PlotNumber", payment.PlotNumber);
                para.Add("@ReceivedAmount", payment.ReceivedAmount);
                para.Add("@PaymentNumber", payment.PaymentNumber);
                para.Add("@CreateDate", DateTime.Now);
                para.Add("@ModifyDate", DateTime.Now);
                para.Add("@check", dbType: DbType.Int32, direction: ParameterDirection.Output);
                int resultMember = con.Execute("sp_InsertPayment", para, sqlTransaction, 0, CommandType.StoredProcedure);
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

        public int InsertMember(MemberRegistration memberRegistration)
        {
            using (var con = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
            {
                con.Open();
                var sqlTransaction = con.BeginTransaction();
                var para = new DynamicParameters();
                //para.Add("@MemberId", "0");
                para.Add("@MemberFName", memberRegistration.MemberFName);
                para.Add("@MemberLName", memberRegistration.MemberLName);
                para.Add("@MemberMName", memberRegistration.MemberMName);
                para.Add("@Address", memberRegistration.Address);
                para.Add("@DOB", memberRegistration.Dob);
                para.Add("@Age", memberRegistration.Age);
                para.Add("@Contactno", memberRegistration.Contactno);
                para.Add("@EmailID", memberRegistration.EmailId);
                para.Add("@Gender", memberRegistration.Gender);           
                para.Add("@Createdby", memberRegistration.Createdby);
                para.Add("@createddate", DateTime.Now);

                //para.Add("@ModifiedBy", 0);
                para.Add("@check", dbType: DbType.Int32, direction: ParameterDirection.Output);
                int resultMember = con.Execute("sp_InsertMember", para, sqlTransaction, 0, CommandType.StoredProcedure);
                int MemberId = para.Get<int>("check");
                if (MemberId > 0)
                {
                    sqlTransaction.Commit();
                    return MemberId;
                }
                else
                {
                    sqlTransaction.Rollback();
                    return 0;
                }
                return 0;
            }
        }

        public int UpdateMember(MemberRegistration memberRegistration)
        {
            using (var con = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
            {
                con.Open();
                var sqlTransaction = con.BeginTransaction();
                var para = new DynamicParameters();
                para.Add("@MemberId", memberRegistration.Id);
                para.Add("@MemberFName", memberRegistration.MemberFName);
                para.Add("@MemberLName", memberRegistration.MemberLName);
                para.Add("@MemberMName", memberRegistration.MemberMName);
                para.Add("@Address", memberRegistration.Address);
                para.Add("@DOB", memberRegistration.Dob);
                para.Add("@Age", memberRegistration.Age);
                para.Add("@Contactno", memberRegistration.Contactno);
                para.Add("@EmailID", memberRegistration.EmailId);
                para.Add("@Gender", memberRegistration.Gender);
                para.Add("@ModifiedBy", 0);
                int result = con.Execute("Usp_UpdateMemberDetails", para, sqlTransaction, 0, CommandType.StoredProcedure);
                if (result > 0)
                {
                    sqlTransaction.Commit();
                    return result;
                }
                else
                {
                    sqlTransaction.Rollback();
                    return 0;
                }
            }
        }

        public IQueryable<MemberRegistrationGridModel> GetAll(QueryParameters queryParameters , int userId)
        {
            IQueryable<MemberRegistrationGridModel> allItems = (from member in _context.MemberRegistration
                                                                                                                               
                                                                select new MemberRegistrationGridModel()
                                                                {
                                                                    Id=member.Id,
                                                                    MemberName = member.MemberFName + '|' + member.MemberMName + '|' + member.MemberLName,                                                                    
                                                                    Contactno = member.Contactno,
                                                                    EmailId = member.EmailId

                                                                });



            if (queryParameters.HasQuery())
            {
                allItems = allItems
                    .Where(x => x.MemberName.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            return allItems
                .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount);
        }

        public IQueryable<MemberPlotGridModel> GetAllPlot(QueryParameters queryParameters, int userId)
        {
            IQueryable<MemberPlotGridModel> allItems = (from member in _context.Plot
                                                        join cmember in _context.MemberRegistration on member.MemberId equals cmember.Id 
                                                        where member.MemberId == userId
                                                                select new MemberPlotGridModel()
                                                                {
                                                                    PlotId=member.PlotId,
                                                                    MemberId=member.MemberId,
                                                                    PlotNo=member.PlotNo,
                                                                    PlotArea=member.PlotArea,
                                                                    RateperMerla=member.RateperMerla,
                                                                    PriceToBeDecided=member.PriceToBeDecided,
                                                                    Total=member.TotalAmount,
                                                                    BookingDate=member.BookingDate,
                                                                    CustomerName=cmember.MemberFName+cmember.MemberMName+cmember.MemberLName,
                                                                    CustomerNumber=cmember.Contactno,
                                                                    BookingAuthority=member.BookingAuthority                                                                  
                                                                });     

            return allItems
                .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount);
        }

        
        public int Count(int userId)
        {
            var membercount = (from member in _context.MemberRegistration
                            where member.Createdby == userId
                select member).Count();
            return membercount;
        }

        public int PlotCount(int userId)
        {
            var membercount = (from member in _context.Plot
                               where member.MemberId == userId
                               select member).Count();
            return membercount;
        }

        public List<MemberResponse> GetMemberNoList(string membername, int userId)
        {
            var membernoList = (from member in _context.MemberRegistration
                                where member.MemberFName.Contains(membername) && member.Createdby == userId
                                select new MemberResponse
                                {
                                    MemberName = member.MemberFName + ' ' + member.MemberMName + ' ' + member.MemberLName
                                }).ToList();

            return membernoList;
        }




    }
}
