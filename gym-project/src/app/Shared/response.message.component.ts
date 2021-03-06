import { Component, Input, ViewEncapsulation } from "@angular/core";
import {} from 'ngx-bootstrap/alert/ngx-bootstrap-alert';
@Component({
  selector: "response-message",
  template: `
    <div *ngIf="message && !errormessage" class="alert alert-success" role="alert">
      <label>{{ message }}</label>
    </div>
    <div *ngIf="errormessage && !message" class="alert alert-danger" role="alert">
    <label>{{errormessage}}</label>
    </div>
  `,
  encapsulation: ViewEncapsulation.None,
})
export class ResponseMessageComponent {
  @Input() message;
  @Input() errormessage;
  @Input() isSuccess;
}
