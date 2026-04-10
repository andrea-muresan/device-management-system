import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Device } from '../../shared/modules/device';
import { DeviceDetails } from '../../shared/modules/device-details';
import { DeviceSummary } from '../../shared/modules/device-summary';
import { DeviceCreate } from '../../shared/modules/device-create';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class InventoryService {
  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);
  
  getDevices() {
    return this.http.get<Device[]>(this.baseUrl + 'devices');
  }

  getDeviceDetails(id: number) {
    return this.http.get<DeviceDetails>(this.baseUrl + 'devices/details/' + id);
  }

  getDevicesSummary() {
    return this.http.get<DeviceSummary[]>(this.baseUrl + 'devices/summary');
  }

  createDevice(device: DeviceCreate): Observable<any> {
    return this.http.post(`${this.baseUrl}devices`, device);
  }
}
