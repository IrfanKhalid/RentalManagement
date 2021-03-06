import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router'; 
import { PaginationService } from '../../Shared/PaginationService';
import { MemberRegistrationModel } from '../Models/app.memberRegistrationModel';
import { MemberRegistrationService } from '../Services/app.MemberRegistration.service';


@Component({
    selector: 'app-list',
    templateUrl: 'Memberlist.component.html',
    styleUrls: ['../../Content/vendor/bootstrap/css/bootstrap.min.css',
        '../../Content/vendor/metisMenu/metisMenu.min.css',
        '../../Content/dist/css/sb-admin-2.css',
        '../../Content/vendor/font-awesome/css/font-awesome.min.css'
    ]
})

export class MemberListComponent {

    dataSource = new MatTableDataSource<MemberRegistrationModel>();
    displayedColumns: string[] = ['MemberName', 'Contactno', 'ViewAction' ,'EditAction', 'DeleteAction','AddPlotAction'];
  
    _Route: any;

    @Input('dataSource')
    set dataSourceForTable(value: MemberRegistrationModel[]) 
    {
        this.dataSource = new MatTableDataSource<MemberRegistrationModel>(value);
    }

    @Input() totalCount: number;
    @Output() onPageSwitch = new EventEmitter();

    constructor(public paginationService: PaginationService,
         private memberregistration: MemberRegistrationService,
         private router: Router) { }

    applyFilter(filterValue: string) {
        this.dataSource.filter = filterValue.trim().toLowerCase();
    }

    AddPlot(id){
        this.router.navigate(['/Member/AttachPlot/',id]);
        // <a class="btn btn-info"  [routerLink]="['/Member/AttachPlot/',element.MemberId]"><i
        //                         class="fa fa-edit fa-fw"></i>
    }
    
    ViewAction(id){
        this.router.navigate(['/Member/View/',id]);
        // <a class="btn btn-info"  [routerLink]="['/Member/AttachPlot/',element.MemberId]"><i
        //                         class="fa fa-edit fa-fw"></i>
    }
    Delete(MemberId): void {
        //console.log(MemberId);
        if (confirm("Are you sure to delete Member ?")) {
            this.memberregistration.DeleteMember(MemberId).subscribe
                (
                    response => {
                        if (response.StatusCode == "200") {
                            alert('Deleted Member Successfully');
                            location.reload();
                        }
                        else {
                            alert('Something Went Wrong');
                            this._Route.navigate(['/Member/AllMember']);
                        }
                    }
                )
        }
    }

}
