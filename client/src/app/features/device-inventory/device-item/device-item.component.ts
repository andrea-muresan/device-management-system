import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { DeviceType } from '../../../shared/models/device-type';
import { MatButton } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { DeviceSummary } from '../../../shared/models/device-summary';
import { InventoryService } from '../../../core/services/inventory.service';

@Component({
  selector: 'app-device-item',
  imports: [
    MatIconModule,
    MatButton,
    RouterLink
  ],
  templateUrl: './device-item.component.html',
  styleUrl: './device-item.component.css',
})
export class DeviceItemComponent {
  @Input() device?: DeviceSummary;
  @Output() deleted = new EventEmitter<number>();
  private deviceService = inject(InventoryService);
  
  get deviceTypeName(): string {
    if (this.device) return DeviceType[this.device.type]; 
    else return '';
  }

  onDelete() {
    if (this.device)
    {
      const idDev = this.device.id;
      if (confirm('Are you sure you want to delete this device?')) {
        this.deviceService.deleteDevice(idDev).subscribe({
          next: () => {
            console.log('Device deleted successfully');
            this.deleted.emit(idDev);
          },
          error: (err) => {
            console.error('Delete failed', err);
            alert('Could not delete the device. It might still be in use.');
          }
        });
      }
    }
  }

  onUpdate() {
    console.log('Delete clicked for:');
  }
}
