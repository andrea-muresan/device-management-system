import { Component, inject, OnInit } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { InventoryService } from '../../../core/services/inventory.service';
import { Device } from '../../../shared/modules/device';
import { ActivatedRoute } from '@angular/router';
import { DeviceDetails } from '../../../shared/modules/device-details';
import { DeviceType } from '../../../shared/modules/device-type';

@Component({
  selector: 'app-device-details',
  imports: [MatIconModule],
  templateUrl: './device-details.component.html',
  styleUrl: './device-details.component.css',
})
export class DeviceDetailsComponent implements OnInit {
  private inventoryService = inject(InventoryService);
  private activatedRoute = inject(ActivatedRoute);
  deviceDetails?: DeviceDetails;
  
  ngOnInit(): void {
    this.loadDevice();
  }

  get deviceTypeName(): string {
      if (this.deviceDetails) return DeviceType[this.deviceDetails.type]; 
      else return '';
  }

  loadDevice() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (!id) return;
    this.inventoryService.getDeviceDetails(+id).subscribe({
      next: device => this.deviceDetails = device,
      error: error => console.log(error)
    });
  }
}
