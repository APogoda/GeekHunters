import {Component, OnInit} from '@angular/core';
import {Candidate, KeyValuePair} from "../../models/candidate";
import {CandidateService} from "../../services/candidate.service";
import {SkillService} from "../../services/skill.service";
import {forEach} from "@angular/router/src/utils/collection";

@Component({
    selector: 'app-candidate-list',
    templateUrl: './candidate-list.component.html',
    styleUrls: ['./candidate-list.component.css']
})
export class CandidateListComponent implements OnInit {
    private readonly PAGE_SIZE = 10;
    queryResult: any = {};
    skills: KeyValuePair[];
    filter: any = {
        pageSize:this.PAGE_SIZE,
        page:1
    };

    constructor(private candidateService: CandidateService, private skillService: SkillService) {
    }

    ngOnInit() {
        this.populateCandidates();
        this.skillService.getSkills("").subscribe(
            skills => {
                this.skills = skills;
            }
        );
    }

    private populateCandidates() {
        this.candidateService.getCandidates(this.filter).subscribe(
            qr => {
                this.queryResult.candidates = qr.items;
                
                this.queryResult.candidates.forEach((element) => {
                    
                    element.skillsString = element.skills.map(function (e) {
                        return e.name;
                    }).join(', ');
                });
                this.queryResult.totalItems = qr.totalItems;
            });
    }
    
    onPageChange(page){
        this.filter.page=page;
        this.populateCandidates();
    }

    onFilterChange() {
        
        this.filter.page = 1;
        this.populateCandidates();
    }

    resetFilter() {
        this.filter = {
            page: 1,
            pageSize: this.PAGE_SIZE
        };
        this.populateCandidates();
    }
    
    delete(id){
        if (confirm("Are you sure?")) {
            this.candidateService.deleteCandidate(id).subscribe(x=>{
                this.resetFilter()});
        }
    }

}
