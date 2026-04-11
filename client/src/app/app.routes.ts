import { Routes } from '@angular/router';
import { DeviceInventoryComponent } from './features/device-inventory/device-inventory.component';
import { DeviceDetailsComponent } from './features/device-details/device-details.component';
import { DeviceAddComponent } from './features/device-add/device-add.component';
import { LoginComponent } from './features/account/login/login.component';
import { RegisterComponent } from './features/account/register/register.component';
import { authGuard } from './core/guards/auth-guard';

export const routes: Routes = [
    {path: '', component: DeviceInventoryComponent, canActivate: [authGuard]},
    {path: 'inventory', component: DeviceInventoryComponent, canActivate: [authGuard]},
    {path: 'account/login', component: LoginComponent},
    {path: 'account/register', component: RegisterComponent},
    {path: 'inventory/add', component: DeviceAddComponent, canActivate: [authGuard]},
    {path: 'inventory/view/:id', component: DeviceDetailsComponent, canActivate: [authGuard]},
    {path: 'inventory/update/:id', component: DeviceAddComponent, canActivate: [authGuard]},
    {path: '**', redirectTo: '', pathMatch: 'full'}
];
