import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { HomeRoutingModule } from './home-routing.module';
import { NewsletterComponent } from './newsletter/newsletter.component';
import { HomeComponent } from './home.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [NewsletterComponent, HomeComponent],
  imports: [CommonModule, SharedModule, HomeRoutingModule, FormsModule, ReactiveFormsModule]
})
export class HomeModule {}
