import {Component, OnInit} from '@angular/core';
import {CandidateService} from "../../services/candidate.service";
import {SkillService} from "../../services/skill.service";
import {Candidate, CreateCandidate} from "../../models/candidate";
import {ActivatedRoute, Router} from "@angular/router";
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/observable/forkJoin';
import {ToastyService} from "ng2-toasty";
import * as _ from 'underscore';

@Component({
    selector: 'app-candidate',
    templateUrl: './candidate.component.html',
    styleUrls: ['./candidate.component.css']
})
export class CandidateComponent implements OnInit {
    skills: any[];
    candidate: CreateCandidate = {
        id: 0,
        firstName: '',
        lastName: '',
        skills: []
    };


    constructor(private route: ActivatedRoute, private candidateService: CandidateService, private skillService: SkillService, private toastyService: ToastyService, private router: Router) {
        route.params.subscribe(
            p => {
                this.candidate.id = +p['id'];
            }
        );
    }

    ngOnInit() {
        let sources = [this.skillService.getSkills("")];
        if (this.candidate.id)
            sources.push(this.candidateService.getCandidate(this.candidate.id));

        Observable.forkJoin(sources).subscribe(data => {
            this.skills = data[0];
            if (this.candidate.id)
                this.setCandidate(data[1]);
        }, error1 => {
        });
    }

    private setCandidate(c: Candidate) {
        this.candidate.id = c.id;
        this.candidate.firstName = c.firstName;
        this.candidate.lastName = c.lastName;
        this.candidate.skills = _.pluck(c.skills, 'id');
    }

    onCandidateChange() {
    }

    onSkillToggle(skillId, $event) {

        if ($event.target.checked)
            this.candidate.skills.push(skillId);
        else {
            let index = this.candidate.skills.indexOf(skillId);
            this.candidate.skills.splice(index, 1);
        }
    }

    submit() {
        if (this.candidate.firstName == "")
            this.showValidationErrorMessage("First Name");
        else if (this.candidate.lastName == "")
            this.showValidationErrorMessage("Last Name");
        else {
            if (this.candidate.id) {
                this.updateCandidate();
            }
            else
                this.createCandidate();
        }
    }

    private createCandidate() {
        this.candidateService.createCandidate(this.candidate).subscribe(
            x => {
                this.router.navigate(['/candidates']);
                this.showSuccessMessage('Candidate has been added');
            },
            error1 => {
                this.showReturnedErrorMessage(error1);
            }
        );
    }

    private showReturnedErrorMessage(error1) {
        this.toastyService.error({
            title: 'Error',
            msg: error1,
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

    private updateCandidate() {
        this.candidateService.updateCandidate(this.candidate).subscribe(
            x => {
                this.showSuccessMessage('Candidate has been updated');
                this.router.navigate(['/candidates']);
            },

            error1 => {
                this.showReturnedErrorMessage(error1);
            }
        )
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
}
