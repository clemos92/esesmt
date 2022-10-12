import { CompletedChecklistItem } from "./completed-checklist-item";

export class CompletedChecklist {
    id: number;
    checklistId: number;
    checklistDescription: string;
    checklistChecklistTypeName: string;
    creationDate: string;
    completedChecklistItems: Array<CompletedChecklistItem>;
}