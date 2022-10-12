import { ChecklistItem } from "./checklist-item";

export class Checklist {
    id: number;
    description: string;
    checklistTypeId?: number;
    checklistTypeName: string;
    checklistItems: Array<ChecklistItem>;
    isActive?: boolean;
}