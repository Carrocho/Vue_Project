import Vue from 'vue'
import Router from 'vue-router'
import Alunos from './components/Aluno/Alunos-A.vue'
import AlunoDetalhe from './components/Aluno/Aluno-Detalhe.vue'
import Professor from './components/Professor/Professor-A.vue'
import Sobre from './components/Sobre/Sobre-A.vue'
import LoginCadastro from './components/LoginCadastro/Login-Cadastro.vue'
import AdminRegistro from './components/AdminRegistro/Admin-Registro.vue'

Vue.use(Router);

export default new Router({
    routes: [
        {
            path: '/',
            redirect: '/login'
        },
        {
            path: '/login',
            nome: 'login',
            component: LoginCadastro,
        },
        {
            path: '/registroADM',
            nome: 'registroADM',
            component: AdminRegistro,
        },
        {
            path: '/professores',
            nome: 'Professores',
            component: Professor,
        },
        {
            path: '/alunos',
            nome: 'Alunos',
            component: Alunos,
        },
        {
            path: '/alunos/:prof_id',
            nome: 'Alunos',
            component: Alunos,
        },
        {
            path: '/alunoDetalhe/:aluno_id',
            nome: 'AlunoDetalhe',
            component: AlunoDetalhe,
        },
        {
            path: '/Sobre',
            nome: 'Sobre',
            component: Sobre,
        }
    ]
})