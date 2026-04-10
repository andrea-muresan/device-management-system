import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { InventoryService } from '../../core/services/inventory.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { DEVICE_TYPE_LIST } from '../../shared/modules/device-type';
import { DeviceCreate } from '../../shared/modules/device-create';

@Component({
  selector: 'app-device-add',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './device-add.component.html',
  styleUrl: './device-add.component.css',
})
export class DeviceAddComponent implements OnInit{
  private fb = inject(FormBuilder);
  private deviceService = inject(InventoryService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  
  readonly deviceTypes = DEVICE_TYPE_LIST;
  
  isEditMode = false;
  deviceId?: number;

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.isEditMode = true;
      this.deviceId = Number(idParam);
      this.loadDeviceData(this.deviceId);
    }
  }
  
  deviceForm = this.fb.group({
    name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
    manufacturer: ['', [Validators.required, Validators.maxLength(50)]],
    type: [null as number|null, [Validators.required]],
    os: ['', [Validators.required, Validators.maxLength(30)]],
    osVersion: ['', [Validators.required, Validators.maxLength(20)]],
    processor: ['', [Validators.required, Validators.maxLength(100)]],
    ram: [8, [Validators.required, Validators.min(1), Validators.max(512)]],
    description: ['', [Validators.required, Validators.maxLength(500)]],
  });

  loadDeviceData(id: number) {
    this.deviceService.getDeviceById(id).subscribe({
      next: (device) => {
        this.deviceForm.patchValue(device as any);
      },
      error: (err) => console.error('Could not load device', err)
    });
  }

 onSubmit() {
  if (this.deviceForm.invalid) return;

  // Combine form values with the ID
  const formValues = this.deviceForm.value;
  
  const payload = {
    ...formValues,
    type: Number(formValues.type),
    id: this.deviceId
  };

  const operation$ = this.isEditMode 
    ? this.deviceService.updateDevice(payload) 
    : this.deviceService.createDevice(payload as DeviceCreate);

  operation$.subscribe({
    next: () => this.router.navigate(['/devices']),
    error: (err) => {
      console.error(`${this.isEditMode ? 'Update' : 'Creation'} failed:`, err);
    }
  });
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
