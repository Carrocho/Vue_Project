<template>
    <div>
        <Titulo texto="Professores" />
        <table>
            <thead>
                <th>Código</th>
                <th>Nome</th>
                <th>Alunos</th>
            </thead>
            <tbody>
                <tr v-for="(professor, index) in professores" :key="index">
                    <td class="colPequeno" style="text-align: center; width: 10%;">{{ professor.id }}</td>
                    <router-link v-bind:to="`/alunos/${professor.id}`" tag="td" v-if="professor.sobrenome.length"  style="cursor: pointer;">
                        {{ professor.nome }} {{professor.sobrenome[0]}}.
                    </router-link>
                    <router-link :to="`/alunos/${professor.id}`" tag="td" v-else style="cursor: pointer;">
                        {{ professor.nome }}
                    </router-link>
                    <td class="colPequeno" style="text-align: center; width: 15%;">
                        {{professor.qtdAlunos}}
                    </td>
                </tr>
            </tbody>
            <tfoot v-if="!professores.length"> Nenhum professor encontrado </tfoot>
        </table>
    </div>
</template>

<script>

import Titulo from '../_share/Titulo-A';

export default {
    components: {
        Titulo,
    },
    data() {
        return {
            professores: [],
            alunos: []
        }
    },
    created() {
        this.$http.get('http://localhost:3000/alunos')
        .then(res => res.json())
        .then(alunos => {
            this.alunos = alunos;
            this.carregarProfessores();
        })
    },
    props: {

    },
    methods:{
        pegarQtdAlunos(){
            this.professores.forEach((professor, index) => {
                professor = {
                    id: professor.id,
                    nome: professor.nome,
                    sobrenome: professor.sobrenome,
                    qtdAlunos: this.alunos.filter(aluno => 
                        aluno.professor.id == professor.id
                    ).length
                }
                console.log(professor),
                this.professores[index] = professor;
            });
        },
        carregarProfessores(){
            this.$http
            .get('http://localhost:3000/professores')
            .then(res => res.json())
            .then(professor => {
                this.professores = professor
                this.pegarQtdAlunos();
            })
        }
    }
}
</script>

<style scoped></style>