import { Component, inject, OnInit } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { InventoryService } from '../../core/services/inventory.service';
import { Device } from '../../shared/models/device';
import { ActivatedRoute } from '@angular/router';
import { DeviceDetails } from '../../shared/models/device-details';
import { DeviceType } from '../../shared/models/device-type';
import { AccountService } from '../../core/services/account.service';

@Component({
  selector: 'app-device-details',
  imports: [MatIconModule],
  templateUrl: './device-details.component.html',
  styleUrl: './device-details.component.css',
})
export class DeviceDetailsComponent implements OnInit {
  private inventoryService = inject(InventoryService);
  private activatedRoute = inject(ActivatedRoute);
  private accountService = inject(AccountService);
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

  assignToMe() {
    if (!this.deviceDetails) return;
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    if (!id) return;
    this.inventoryService.assignDevice(+id).subscribe({
      next: () => {
        this.loadDevice(); 
      },
      error: (err) => {
        console.error('Assignment failed', err);
        alert('Could not assign device. Make sure you are logged in!');
      }
    });
  }

  unassign() {
    if (!this.deviceDetails) return;
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    
    if (!id) return;
    if (confirm('Are you sure you want to release this device back to the pond?')) {
      this.inventoryService.unassignDevice(+id).subscribe({
        next: () => this.loadDevice(),
        error: (err) => console.error(err)
      });
    }
  }

  isAssignedToMe() {
    const currentUser = this.accountService.currentUser();

    if (!this.deviceDetails || !currentUser) return false;
    if (!this.deviceDetails.userEmail) return false;

    return this.deviceDetails.userEmail === currentUser.email;
  }
}
