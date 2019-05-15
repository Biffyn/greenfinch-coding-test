import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, AbstractControl } from '@angular/forms';
import { NewsletterService } from './services/newsletter.service';
import { SnotifyService } from 'ng-snotify';
import { ApiResponse } from 'src/app/shared/models/api-respone.model';
import { ServerValidationError } from 'src/app/shared/models/server-validation-error.model';
import { ValidationService } from 'src/app/shared/services/validation.service';

@Component({
  selector: 'app-newsletter',
  templateUrl: './newsletter.component.html',
  styleUrls: ['./newsletter.component.scss']
})
export class NewsletterComponent implements OnInit {
  public newsletterForm: FormGroup;
  private snotifyConfig = {
    timeout: 3000,
    showProgressBar: true,
    closeOnClick: false,
    pauseOnHover: true
  };

  constructor(
    private fb: FormBuilder,
    private newsletterService: NewsletterService,
    private snotify: SnotifyService,
    private validation: ValidationService
  ) {}

  ngOnInit() {
    this.newsletterForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      referrer: [null, [Validators.required]],
      reason: ['', []],
      form: ['', []]
    });
  }

  public get email() {
    return this.newsletterForm.get('email');
  }

  public get referrer() {
    return this.newsletterForm.get('referrer');
  }

  public get reason() {
    return this.newsletterForm.get('reason');
  }

  public onSubmit() {
    if (!this.newsletterForm.valid) {
      return;
    } else {
      this.newsletterService.subscribe(this.newsletterForm.value).subscribe(
        (response: ApiResponse<boolean>) => {
          if (response.data) {
            this.snotify.success('Thank you for signing up to our newsletter.', 'Subscribed', this.snotifyConfig);
            this.newsletterForm.reset();
          } else {
            if (response.errors) {
              const errors = response.errors as ServerValidationError[];
              this.validation.setErrors(this.newsletterForm, errors);
              this.snotify.error('Something went wrong while subscribing.', 'Unsuccessful', this.snotifyConfig);
            }
          }
        },
        (error) => {
          console.error(error);
        }
      );
    }
  }
}
