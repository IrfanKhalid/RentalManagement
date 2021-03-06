import { Component, OnInit } from '@angular/core';
import { SchemeService } from '../SchemeMasters/Services/app.Scheme.Service';
import { Router, ActivatedRoute } from '@angular/router';
import { SchemeDropdownModel } from '../SchemeMasters/Models/app.SchemeDropdownModel';
import { PlanService } from '../PlanMaster/Services/app.planmaster.service';
import { MemberRegistrationModel } from './Models/app.memberRegistrationModel';
import { MemberRegistrationService } from './Services/app.MemberRegistration.service';
import { ActivePlanModel } from '../PlanMaster/Models/app.ActivePlanModel';
import { DatePipe } from '@angular/common';
import { PlotGridModel } from './Models/aap.PlotGridModel';
import { MemberPlotListComponent} from './app.MemberPlotListComponent';
import { PageEvent } from '@angular/material/paginator';
import { PaginationService } from '../Shared/PaginationService';
@Component({
    templateUrl: './app.ViewMemberRegistration.html',
    styleUrls: ['../Content/vendor/bootstrap/css/bootstrap.min.css',
        '../Content/vendor/metisMenu/metisMenu.min.css',
        '../Content/dist/css/sb-admin-2.css',
        '../Content/vendor/font-awesome/css/font-awesome.min.css'
    ]
})


export class ViewMemberRegistrationComponent implements OnInit 
{
    dataSource: PlotGridModel[];
    totalCount: number;

    MemberModel: MemberRegistrationModel = new MemberRegistrationModel();
    Plots : PlotGridModel= new PlotGridModel();
    errorMessage: any;
    output: any;
    genderList: any[];
    bsRangeValue: Date[];
    bsValue = new Date();
    private _memberregistration;
    public age: number;
    MemberID: any;
 
    constructor(
        private _Route: Router,
        private _routeParams: ActivatedRoute,
        private memberregistration: MemberRegistrationService,
        private paginationService: PaginationService
    ) {
        this._memberregistration = memberregistration;
    }

    ngOnInit(): void 
    {
        this.MemberID = this._routeParams.snapshot.params['MemberId'];
          
        // this._memberregistration.GetAllActiveSchemeList().subscribe(
        //     allActiveScheme => {
        //         this.AllActiveSchemeList = allActiveScheme
        //     },
        //     error => this.errorMessage = <any>error
        // );

        if(this.MemberID !=null)
        {
            this._memberregistration.GetMemberById(this.MemberID).subscribe(
                memberModel => 
                {
                    this.MemberModel = memberModel

                },
                error => this.errorMessage = <any>error
            );
            this.getAllPlot();            
        }
        
    }


    switchPage(event: PageEvent) {
        this.paginationService.change(event);
        this.getAllPlot();
    }

    getAllPlot() {
        this.memberregistration.GetAllPlotsByMemberId<PlotGridModel[]>(this.MemberID)
            .subscribe((result: any) => 
            {
                console.log(result.headers);
                this.totalCount = JSON.parse(result.headers.get('X-Pagination')).totalCount;
               // this.totalCount = 4;
                this.dataSource = result.body.value;
            });           
    }


    onSubmit() {

        let demo = this.bsValue
        this._memberregistration.UpdateMember(this.MemberModel).subscribe(
            response => {
                this.output = response
                if (this.output.StatusCode == "409") {
                    alert('Member Already Exists');
                }
                else if (this.output.StatusCode == "200") {
                    alert('Member Details Updated Successfully');
                    this._Route.navigate(['/Member/All']);
                }
                else {
                    alert('Something Went Wrong');
                }
            });

       
    }
   
   
    numberOnly(event): boolean 
    {
        const charCode = (event.which) ? event.which : event.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;

    }

    // GetAmount(PlanID: number, SchemeID: number) {
    //     if (PlanID != null && SchemeID != null) {
    //         this._memberregistration.GetAmount(PlanID, SchemeID).subscribe(
    //             amount => {
    //                 this.MemberModel.Amount = amount
    //             },
    //             error => this.errorMessage = <any>error
    //         );
    //     }
    // }
  
   


}
