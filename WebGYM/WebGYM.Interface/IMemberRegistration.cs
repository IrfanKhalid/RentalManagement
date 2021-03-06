using System.Collections.Generic;
using System.Linq;
using WebGYM.Models;
using WebGYM.ViewModels;

namespace WebGYM.Interface
{
    public interface IMemberRegistration
    {
        int InsertMember(MemberRegistration memberRegistration);
        int InsertAttachPlot(AttahPlot attahPlot);

        long CheckNameExitsforUpdate(string memberFName, string memberLName, string memberMName);
        bool CheckNameExits(string memberFName ,string memberLName, string memberMName);
        List<MemberRegistrationGridModel> GetMemberList();
        MemberRegistrationViewModel GetMemberbyId(int memberId);
        bool DeleteMember(long memberId);
        int UpdateMember(MemberRegistration memberRegistration);
        IQueryable<MemberRegistrationGridModel> GetAll(QueryParameters queryParameters, int userId);
        IQueryable<MemberPlotGridModel> GetAllPlot(QueryParameters queryParameters, int userId);

        int Count(int userId);

        int PlotCount(int userId);        
        List<MemberResponse> GetMemberNoList(string memberNo, int userId);
        int InsertPayment(PaymentDetails automember);
    }
}