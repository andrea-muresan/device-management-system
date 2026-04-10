import { Component, Input } from '@angular/core';
import { Device } from '../../../shared/modules/device';
import { MatIconModule } from '@angular/material/icon';
import { DeviceType } from '../../../shared/modules/device-type';
import { MatButton } from '@angular/material/button';
import { RouterLink } from '@angular/router';

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
  @Input() device?: Device;
  
  get deviceTypeName(): string {
    if (this.device) return DeviceType[this.device.type]; 
    else return '';
  }

  onDelete() {
    console.log('Delete clicked for:');
  }

  onUpdate() {
    console.log('Delete clicked for:');
  }
}
