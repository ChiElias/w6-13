import { Component, OnInit } from '@angular/core'
import { AccountService } from '../_services/account.service'
import { Observable, of } from 'rxjs'
import { User } from '../_modles/user'
import { Router } from '@angular/router'
import { ToastrService } from 'ngx-toastr'


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})

export class NavComponent implements OnInit {
  model: any = {}
  currentUser$: Observable<User | null> = of(null) // isLogin = false
  user : User | null = null

  constructor(private toastr: ToastrService, private router: Router, public accountService: AccountService){ }

  
  ngOnInit(): void {
      this.currentUser$ = this.accountService.currentUser$
      this.currentUser$.subscribe({
        next: user => this.user = user
      })
  }
  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
        // next: user => this.isLogin = !!user, // user?true:false
        error: err => console.log(err)
    })
}
login(): void {
   this.accountService.login(this.model).subscribe({
          next: _ => this.router.navigateByUrl('/members'),
        //  error: err => this.toastr.error(err)
  })
}
logout() {
  this.accountService.logout()
  this.router.navigateByUrl('/')
}
}

// export class NavComponent {
//   model: { username: string | undefined, password: string | undefined } = {
//       username: undefined,
//       password: undefined
//   }
//   isLogin = false

//   constructor(private accountService: AccountService) { }

//   login(): void {
//       this.accountService.login(this.model).subscribe({ //Observable
//           next: response => {
//               console.log(response)
//               this.isLogin = true
//           },
//           error: err => console.log(err) //anything that's not in 200 range of HTTP status
//       })
//   }
// }
