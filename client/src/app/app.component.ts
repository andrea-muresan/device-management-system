import { Component, inject, OnInit } from '@angular/core';
import { HeaderComponent } from "./layout/header/header.component";
import { HttpClient } from '@angular/common/http';
import { Device } from './shared/modules/device';

@Component({
  selector: 'app-root',
  imports: [HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);
  title = 'DevSys';
  devices: Device[] = [];

  ngOnInit(): void {
    this.http.get<Device[]>(this.baseUrl + 'devices').subscribe({
      next: response => this.devices = response,
      error: error => console.log(error),
      complete: () => console.log('complete')
    });
  }
}
