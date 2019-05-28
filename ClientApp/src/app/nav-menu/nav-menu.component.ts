import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { RouterService } from '../services/router.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  logoutVisible=false;
  constructor(private authService: AuthenticationService, private routerService:RouterService) {
    authService.validateToken.subscribe(n => this.logoutVisible = n);
   }

  ngOnInit(): void {
  }

  collapse() {
    this.isExpanded = false;
  }

  logout()
  {
    console.log("Logging out...");
    this.authService.clearBearerToken();
    this.routerService.routeToLogin();
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
