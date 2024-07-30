import { HttpErrorResponse } from '@angular/common/http';
import { Directive, ElementRef, EventEmitter, HostListener, Input, Output, Renderer2 } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { error } from 'console';
import { NgxSpinner, NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from 'src/app/base/base.component';
import { DeleteDialogComponent, DeleteState } from 'src/app/dialogs/delete-dialog/delete-dialog.component';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import { HttpClientService } from 'src/app/services/common/http-client.service';
import { ProductService } from 'src/app/services/common/models/product.service';

declare var $: any;

@Directive({
  selector: '[appDelete]'
})
export class DeleteDirective {

  constructor(
    private element: ElementRef,
    private _render: Renderer2,
    private httpClientService: HttpClientService,
    public dialog: MatDialog,
    private alertifyService: AlertifyService
  ) {
    const img = _render.createElement("img");
    img.setAttribute("src", "../../../../../assets/delete.png");
    img.setAttribute("style", "cursor: pointer;");
    img.width = 25;
    img.height = 25;
    _render.appendChild(element.nativeElement, img);
  }

  @Input() id: string;
  @Input() controller: string;
  @Output() callback: EventEmitter<any> = new EventEmitter();

  @HostListener("click")
  async onclick() {
    this.openDialog(async () => {
      const td: HTMLTableCellElement = this.element.nativeElement;
      $(td.parentElement).fadeOut(1000);
      this.httpClientService.delete({
        controller: this.controller
      }, this.id).subscribe(data => {
        this.callback.emit();
        this.alertifyService.message("Ürün Başarıyla Silindi", {
          dismissOthers: true,
          messageType: MessageType.Success,
          position: Position.TopRight
        })
      },(errorResponse: HttpErrorResponse)=>{
        this.alertifyService.message("Ürün Başarıyla Silindi", {
          dismissOthers: true,
          messageType: MessageType.Success,
          position: Position.TopRight
        })
      });
    });
  }

  openDialog(afterClosed: any): void {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '250px',
      data: DeleteState.Yes,
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result == DeleteState.Yes) {
        afterClosed();
      }
    });
  }
}
