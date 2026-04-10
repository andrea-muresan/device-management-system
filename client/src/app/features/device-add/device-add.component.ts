import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { InventoryService } from '../../core/services/inventory.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { DEVICE_TYPE_LIST } from '../../shared/modules/device-type';
import { DeviceCreate } from '../../shared/modules/device-create';

@Component({
  selector: 'app-device-add',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './device-add.component.html',
  styleUrl: './device-add.component.css',
})
export class DeviceAddComponent {
  private fb = inject(FormBuilder);
  private deviceService = inject(InventoryService);
  private router = inject(Router);
  readonly deviceTypes = DEVICE_TYPE_LIST;
  
  deviceForm = this.fb.group({
    name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
    manufacturer: ['', [Validators.required, Validators.maxLength(50)]],
    type: [null, [Validators.required]],
    os: ['', [Validators.required, Validators.maxLength(30)]],
    osVersion: ['', [Validators.required, Validators.maxLength(20)]],
    processor: ['', [Validators.required, Validators.maxLength(100)]],
    ram: [8, [Validators.required, Validators.min(1), Validators.max(512)]],
    description: ['', [Validators.required, Validators.maxLength(1000)]],
  });

  onSubmit() {
    if (this.deviceForm.valid) {
        const payload = {
        ...this.deviceForm.value,
        type: Number(this.deviceForm.value.type) 
      };
      this.deviceService.createDevice(payload as DeviceCreate).subscribe({
        next: () => this.router.navigate(['/devices']),
        error: (err) => console.error('Save failed', err)
      });
    }
  }

  getInputClass(controlName: string) {
    const control = this.deviceForm.get(controlName);
    // neutral
    if (control?.pristine && !control?.touched) return 'border-yellow-200';
    // red
    if (control?.invalid && (control?.dirty || control?.touched)) return 'border-red-500 bg-red-50';
    // green
    if (control?.valid && (control?.dirty || control?.touched)) return 'border-green-500 bg-green-50/30';
    
    return 'border-yellow-200';
  }

  shouldShowError(controlName: string): boolean {
    const control = this.deviceForm.get(controlName);
    return !!(control && control.invalid && (control.dirty || control.touched));
  }
}
