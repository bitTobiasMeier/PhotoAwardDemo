
import { CommentUploadData, PhotoManagementClient } from './../../Shared/Controllers.generated';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { UserService } from "../../Shared/user.service";

@Component({
  selector: 'pac-add-comment',
  templateUrl: './add-comment.component.html',
  styles: []
})
export class AddCommentComponent implements OnInit {

  @Input() photoId:string;
  @Output() commentAdded = new EventEmitter<any>();
  @Output() commentCanceled = new EventEmitter<any>();

  data:CommentUploadData;
  message:string;
  constructor(private userservice:  UserService, private photoManagementClient: PhotoManagementClient) { }

  ngOnInit() {
    this.data = new CommentUploadData();
    this.data.photoId = this.photoId;
    this.data.email = this.userservice.user.email;
  }

  save(){
    const that = this;
    this.photoManagementClient.addComment (this.data).subscribe(
             e=> {
               that.message="Kommentar wurde gespeichert";
               this.commentAdded.emit();
               this.message = "";
               this.data.comment = "";
             }           ,
             error => {
                that.message ="Kommentar konnte nicht gespeichert werden.: " + error;
             }
           );
  }

  cancel(){
    this.commentCanceled.emit();
    this.message = "";
  }

}
