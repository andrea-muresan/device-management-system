import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AccountService } from '../../../core/services/account.service';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, CommonModule, RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  private fb = inject(FormBuilder);
  private accountService = inject(AccountService);
  private router = inject(Router);
  registrationSuccess = false;

  registerForm = this.fb.group({
    name: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    role: ['', [Validators.required, Validators.maxLength(50)]],
    location: ['', [Validators.required, Validators.maxLength(100)]]
  });

  onSubmit() {
    if (this.registerForm.invalid) return;

    this.accountService.register(this.registerForm.value).subscribe({
      next: () => {
        this.registrationSuccess = true;

        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 3000);
      },
      error: (err) => {
        console.error(err);
        alert('Registration failed. Email might already be in use.');
      }
    });
  }

  getInputClass(controlName: string) {
    const control = this.registerForm.get(controlName);
    if (control?.invalid && (control?.dirty || control?.touched)) return 'border-red-500 bg-red-50';
    if (control?.valid && (control?.dirty || control?.touched)) return 'border-green-500 bg-green-50/30';
    return 'border-yellow-200';
  }
}
