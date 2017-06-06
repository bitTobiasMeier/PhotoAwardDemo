import { Observable } from 'rxjs/Observable';
import { PhotoManagementClient, CommentData } from './../../Shared/Controllers.generated';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'pac-show-comments',
  templateUrl: './show-comments.component.html',
  styles: []
})
export class ShowCommentsComponent implements OnInit {
  @Input() photoId:string;
  comments:CommentData[];
  showAddCommentSection:boolean = false;
  constructor(private photomanagementclient: PhotoManagementClient) { }

  ngOnInit() {
    this.loadComments();
  }

  private loadComments () {
    this.photomanagementclient.getComments(this.photoId).subscribe(
      (comments )=> {
        this.comments = comments;
      }
    );
  }

  newComment() {
    this.showAddCommentSection = true;
  }

  commentAdded () {
    this.showAddCommentSection = false;
    this.loadComments();
  }

  commentCanceled () {
    this.showAddCommentSection = false;
  }
}
