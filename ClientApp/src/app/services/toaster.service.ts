import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ToasterCustomService {

  constructor(private toastr: ToastrService) { }

  public showSuccess(message: string, title?: string) {

    this.toastr.success(message, '', {
      timeOut: 3000,
      closeButton: true,
      positionClass: 'toast-top-right',
      tapToDismiss: true
    });
  }

  public showError(message: string, title?: string) {
    this.toastr.error(message, '', {
      timeOut: 5000,
      closeButton: true,
      positionClass: 'toast-top-right',
      tapToDismiss: true,
      enableHtml:true
    });
  }

  public showInfo(message: string, title?: string) {
    this.toastr.info(message, '', {
      timeOut: 3000,
      closeButton: true,
      positionClass: 'toast-top-right',
      tapToDismiss: true
    });
  }

  public showWarning(message: string, title?: string) {
    this.toastr.warning(message, '', {
      timeOut: 3000,
      closeButton: true,
      positionClass: 'toast-top-right',
      tapToDismiss: true
    });
  }
}
