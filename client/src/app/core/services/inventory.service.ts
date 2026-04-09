import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Device } from '../../shared/modules/device';

@Injectable({
  providedIn: 'root',
})
export class InventoryService {
  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);
  
  getDevices() {
    return this.http.get<Device[]>(this.baseUrl + 'devices');
  }
}
