import { Tag } from "./tag";
import { Reminder } from "./reminder";

export class Note {
  id: Number;
  title: string;
  content: string;
  state: string;
  userid: Number;
  tags: Array<string>;
  category: Tag;
  reminders: Array<Reminder>;

  constructor() {
    this.category = new Tag();
    this.reminders = Array<Reminder>();
    this.title = '';
    this.content = '';
    this.state = 'not-started'; // default state
  }
}


export class NoteMapper {
  id: Number;
  title: string;
  content: string;
  userid: Number;
  reminders: Array<Reminder>;
  category: Tag;
  
  constructor() {
    this.title = '';
    this.content = '';
  }
}