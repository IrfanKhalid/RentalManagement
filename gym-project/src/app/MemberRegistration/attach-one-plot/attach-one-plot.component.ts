import { Component, OnInit,Input, ViewEncapsulation  } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MemberRegistrationService } from '.././Services/app.MemberRegistration.service';
import { DatePipe } from '@angular/common';
import { PlotModel } from '.././Models/aap.PlotModel';
@Component({
  selector: 'app-attach-one-plot',
  templateUrl: './attach-one-plot.component.html',
  styleUrls: ['./attach-one-plot.component.css',
              '../../Content/vendor/bootstrap/css/bootstrap.min.css',
              '../../Content/vendor/metisMenu/metisMenu.min.css',
              '../../Content/dist/css/sb-admin-2.css',
              '../../Content/vendor/font-awesome/css/font-awesome.min.css']
})
export class AttachOnePlotComponent implements OnInit {

  _PlotModel: PlotModel = new PlotModel();
    @Input() message;
    @Input() errormessage;
    output: any;
    genderList: any[];
    bsRangeValue: Date[];
    bsValue = new Date();
    joinminDate: Date;
    joinmaxDate: Date;        
    isSuccess=true;
    dobminDate: Date;
    MemberID: any; 
    private _memberregistration;

  constructor( private datePipe: DatePipe,
    private _Route: Router,  
    private _routeParams: ActivatedRoute,    
    private memberregistration: MemberRegistrationService)
    {
      this._memberregistration = memberregistration;

    }

  ngOnInit() {
    this.MemberID = this._routeParams.snapshot.params['MemberId'];
    

  }
  onSubmit() {


    console.log(this._PlotModel);
    let demo = this.bsValue
    this._memberregistration.SavePlot(this._PlotModel).subscribe(
        response => 
        {
            this.output = response
            if (this.output.StatusCode == "409") {
                this.message="Plot is already added";
                this.isSuccess=true;
                this.errormessage=null;
                //alert('Member Already Exists');
            }
            else if (this.output.StatusCode == "200") {
                this.message="Plot Added Successfully";
                this.isSuccess=true;
                this.errormessage=null;
                //alert('Member Added Successfully');
                //this._Route.navigate(['/Member/All']);
            }
            else {
                this.errormessage="Something Went Wrong";
                this.message=null;
                //alert('Something Went Wrong');
            }
        });

   
}



numberOnly(event): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;

}


}
