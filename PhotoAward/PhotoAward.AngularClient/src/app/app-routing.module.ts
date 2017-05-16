import { ShowPhotosComponentComponent } from './photos/show-photos-component/show-photos-component.component';
import { UploadPhotoComponentComponent } from './photos/upload-photo-component/upload-photo-component.component';
import { LoginComponent } from './login/login/login.component';
import { RegisterMemberComponent } from './login/register-member/register-member.component';
import { NgModule } from '@angular/core'; import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [{ path: '', component: LoginComponent, pathMatch: 'full' },
  { path: 'registerMember', component: RegisterMemberComponent },
    { path: 'upload', component: UploadPhotoComponentComponent },
    { path: 'gallery', component: ShowPhotosComponentComponent}
  ];
@NgModule({

  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: []
})
export class AppRoutingModule { }
