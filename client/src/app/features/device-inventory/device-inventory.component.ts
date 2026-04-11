import { Component, inject, OnInit } from '@angular/core';
import { DeviceItemComponent } from "./device-item/device-item.component";
import { InventoryService } from '../../core/services/inventory.service';
import { DeviceSummary } from '../../shared/models/device-summary';

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

  handleDelete(id: number) {
    this.devices = this.devices.filter(d => d.id !== id);
  }
}
