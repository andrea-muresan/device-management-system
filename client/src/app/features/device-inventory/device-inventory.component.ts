import { Component, inject, OnInit } from '@angular/core';
import { Device } from '../../shared/modules/device';
import { HttpClient } from '@angular/common/http';
import { DeviceItemComponent } from "./device-item/device-item.component";
import { InventoryService } from '../../core/services/inventory.service';
import { DeviceSummary } from '../../shared/modules/device-summary';

@Component({
  selector: 'app-device-inventory',
  imports: [DeviceItemComponent],
  templateUrl: './device-inventory.component.html',
  styleUrl: './device-inventory.component.css',
})
export class DeviceInventoryComponent implements OnInit {
  baseUrl = 'https://localhost:5001/api/';
  private inventoryService = inject(InventoryService);
  title = 'DevSys';
  devices: DeviceSummary[] = [];

  ngOnInit(): void {
    this.inventoryService.getDevicesSummary().subscribe({
      next: response => this.devices = response,
      error: error => console.log(error)
    });
  }
}
