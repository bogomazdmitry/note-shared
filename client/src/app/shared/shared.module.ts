import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AppBootstrapModule } from '../app-bootstrap.module';
import { AppMaterialModule } from '../app-material.module';
import { FooterComponent } from './layout/footer/footer.component';
import { HeaderComponent } from './layout/header/header.component';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
  ],
  imports: [
    CommonModule,
    AppBootstrapModule,
    AppMaterialModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
  ],
  exports: [
    CommonModule,
    AppMaterialModule,
    AppBootstrapModule,
    HeaderComponent,
    FooterComponent,
    FormsModule,
    ReactiveFormsModule,
  ]
})
export class SharedModule { }
