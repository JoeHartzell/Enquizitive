import Dexie, { type Table } from "dexie";

export interface Session {
  id?: number;
  name: string;
  description?: string;
  globalId?: string;
}

export class EnquizitiveDB extends Dexie {
  sessions: Table<Session, Session["id"]>;

  constructor() {
    super("EnquizitiveDB");
    this.version(1).stores({
      sessions: "++id, name, description, globalId"
    });
    this.sessions = this.table("sessions");
  }
}

export const db = new EnquizitiveDB();
