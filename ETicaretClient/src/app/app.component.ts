import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CustomTostrService, ToastrMessageType, ToastrPosition } from './services/ui/custom-tostr.service';
declare var $: any

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ETicaretClient';
  constructor(){
   
  }
}
$.get("https://localhost:44300/api/Products/Get", data=> {
  console.log(data);
})

