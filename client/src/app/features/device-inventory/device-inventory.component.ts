import { Component, inject, OnInit } from '@angular/core';
import { DeviceItemComponent } from "./device-item/device-item.component";
import { InventoryService } from '../../core/services/inventory.service';
import { DeviceSummary } from '../../shared/models/device-summary';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-device-inventory',
  imports: [DeviceItemComponent, MatButton, MatIcon],
  templateUrl: './device-inventory.component.html',
  styleUrl: './device-inventory.component.css',
})
export class DeviceInventoryComponent implements OnInit {
  baseUrl = 'https://localhost:5001/api/';
  private inventoryService = inject(InventoryService);
  title = 'DevSys';
  devices: DeviceSummary[] = [];
  isSearching = false;

  ngOnInit(): void {
    this.loadInitialDevices();
  }

  handleDelete(id: number) {
    this.devices = this.devices.filter(d => d.id !== id);
  }

  onSearch(term: string): void {
    const query = term.trim();
    
    if (!query) {
      this.loadInitialDevices();
      return;
    }

    this.isSearching = true;
    this.inventoryService.getRankedSearch(query).subscribe({
      next: (response: DeviceSummary[]) => {
        this.devices = response;
        this.isSearching = false;
      },
      error: (err: any) => {
        console.error('Search failed', err);
        this.isSearching = false;
      }
    });
  }

  loadInitialDevices() {
    this.inventoryService.getDevicesSummary().subscribe({
      next: response => this.devices = response,
      error: error => alert(error)
    });
  }
}
