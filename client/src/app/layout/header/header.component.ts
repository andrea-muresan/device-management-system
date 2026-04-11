import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { Router, RouterLink } from "@angular/router";
import { AccountService } from '../../core/services/account.service';

@Component({
  selector: 'app-header',
  imports: [
    MatButton,
    RouterLink
],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent {
  public accountService= inject(AccountService);
  private router = inject(Router);

  logout() {
    this.accountService.logout();
    this.router.navigate(['/login']);
  }

}
