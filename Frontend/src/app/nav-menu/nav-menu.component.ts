import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from '../../services/book.service';

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  imports: [],
  templateUrl: './nav-menu.component.html',
  styleUrl: './nav-menu.component.css'
})
export class NavMenuComponent {

  constructor(public dataService: DataService) { }
}
