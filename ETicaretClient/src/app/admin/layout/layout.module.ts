import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from './layout.component';
import { ComponentsComponent } from './components/components.component';



@NgModule({
  declarations: [
    LayoutComponent
  ],
  imports: [
    CommonModule,
    ComponentsComponent
  ],
  exports:[
    LayoutComponent
  ]
})
export class LayoutModule { }
