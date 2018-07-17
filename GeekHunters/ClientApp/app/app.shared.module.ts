import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { CandidateComponent } from './components/candidate/candidate.component';
import {CandidateService} from "./services/candidate.service";
import {SkillService} from "./services/skill.service";
import {ToastyModule, ToastyService} from "ng2-toasty";
import {AlertModule} from "ngx-bootstrap";
import { CandidateListComponent } from './components/candidate-list/candidate-list.component';
import {PaginationComponent} from "./components/shared/pagination.component";
import { SkillComponent } from './components/skill/skill.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CandidateComponent,
        CandidateListComponent,
        PaginationComponent,
        SkillComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ToastyModule.forRoot(),
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent},
            { path: 'candidates', component: CandidateListComponent },
            { path: 'skills', component: SkillComponent },
            { path: 'candidates/new', component: CandidateComponent },
            { path: 'candidates/:id', component: CandidateComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers : [
        CandidateService,
        SkillService,
        ToastyService
    ]
})
export class AppModuleShared {
}
