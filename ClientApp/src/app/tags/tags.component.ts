import { Component, OnInit } from '@angular/core';
import { ToasterCustomService } from '../services/toaster.service';
import { TagService } from '../services/tag.service';
import { Tag } from '../models/tag';
import { NgForm } from '@angular/forms';
@Component({
  selector: 'app-tags',
  templateUrl: './tags.component.html',
  styleUrls: ['./tags.component.css']
})

export class TagsComponent implements OnInit {

  tag: Tag = new Tag();
  tags: Array<Tag> = [];

  constructor(private tagService: TagService, private toasterService: ToasterCustomService) { }

  ngOnInit() {
    this.getTags();
  }

  getTags() {

    this.tagService.fetchTagsFromServer().subscribe(data => {

      this.tags = data;
      //console.log(this.tags);
    },
    err => {
      this.toasterService.showError(err.error==null ? err.message : err.error);
    }
    );

    //console.log(this.tags);
  }

  addTag(form: NgForm) {
    const formData = this.tag;
    event.preventDefault();

    //console.log(formData);

    if (!formData.name || formData.name.length === 0 || !formData.description || formData.description.length === 0) {
      this.toasterService.showError('Name and Description both are required fields');
    } else {

      var result = this.tagService.getTagByName(this.tag.name);

      if (result) {
        this.toasterService.showError("Tag is already exist with same name.");
      }
      else {
        //this.notes.push(this.note);
        this.tagService.addTag(this.tag).subscribe(
          data => {
            this.toasterService.showSuccess('Tag added successfully.');

            //console.log('addTag: ' + data);
            this.tag = new Tag();
            form.reset();
            this.getTags();
          },
          error => {
            if (error.status === 404) {
              this.toasterService.showError(error.error==null ? error.message : error.error);
            } else {
              this.toasterService.showError(error.error==null ? error.message : error.error);
            }
          }
        );

      }
    }
  }

  deleteTag(event, tagId) {
    //console.log(tagId);
    this.tagService.deleteTag(tagId).subscribe(
      data => {
        //this.getNotes();
        this.toasterService.showSuccess('Tag Deleted Successfully.');
        this.getTags();
       },
      error => {
        //console.log(JSON.stringify(error));
        this.toasterService.showError(error.error==null ? error.message : error.error);
      }
    );
  }
}
