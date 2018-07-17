import {Component, OnInit} from '@angular/core';
import {SkillService} from "../../services/skill.service";
import {KeyValuePair} from "../../models/candidate";
import {ToastyService} from "ng2-toasty";

@Component({
    selector: 'app-skill',
    templateUrl: './skill.component.html',
    styleUrls: ['./skill.component.css']
})
export class SkillComponent implements OnInit {
    private readonly PAGE_SIZE = 10;
    queryResult: any = {};
    skill : KeyValuePair={
        id:0,
        name : ''
    };
    filter: any = {
        pageSize: this.PAGE_SIZE,
        page: 1
    };

    constructor(private skillService: SkillService,private toastyService: ToastyService) {
    }

    ngOnInit() {
        this.populateSkillList();
    }

    private populateSkillList() {
        this.skillService.getSkills(this.filter).subscribe(
            qr => {
                this.queryResult.skills = qr;
                this.queryResult.totalItems = 20;
            });
    }

    submit() {
        if (!   this.skill.name)
            this.showValidationErrorMessage("Name");
        else {
                this.skillService.createSkill(this.skill).subscribe(x=>{
                    this.showSuccessMessage('Skill has been added');
                    this.populateSkillList();
                    this.skill.name='';
                });
        }
    }

    delete(id){
        if (confirm("Are you sure?")) {
            this.skillService.deleteSkill(id).subscribe(x=>{
                this.populateSkillList()});
        }
    }
    
    private showValidationErrorMessage(msg: string) {
        this.toastyService.error({
            title: 'Error',
            msg: msg + " can not be empty",
            theme: 'bootstrap',
            showClose: true,
            timeout: 5000
        });
    }

    private showSuccessMessage(msg: string) {
        this.toastyService.success({
            title: 'Sweet',
            msg: msg,
            theme: 'bootstrap',
            showClose: true,
            timeout: 5000
        });
    }

    onPageChange(page){
        this.filter.page=page;
        this.populateSkillList();
    }

}
