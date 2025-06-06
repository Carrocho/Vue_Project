<template>
    <div>
        <Titulo texto="Professores" :boolVoltar="true" />
        <input ref="input" type="file" style="display: none;" accept="image/*" @change="handleFile($event)">
        <div>
            <input type="text" placeholder="Nome do professor" v-model="nome">
            <button class="btn btnInput" @click="adicionarProfessor()">Adicionar</button>
            <input type="text" placeholder="CPF do professor" v-model="cpfBase"
                @input="cpfBase = formatarCPF($event.target.value)" maxlength="14" />
            <div v-if="cpfInvalido" style="color: red; margin-bottom: 10px; display: inline;">
                CPF inválido!
            </div>
            <div v-if="cpfExistente" style="color: red; margin-bottom: 10px; display: inline;">
                CPF já existe!
            </div>
        </div>
        <table>
            <thead>
                <th>Código</th>
                <th>CPF</th>
                <th>Nome</th>
                <th>Alunos</th>
            </thead>
            <tbody>
                <tr v-for="(professor, index) in professores" :key="index">
                    <td class="colPequeno" style="text-align: center; width: 10%;">{{ professor.id }}</td>
                    <td class="colPequeno" style="text-align: center; width: 20%;">{{ professor.cpf }}</td>
                    <router-link v-bind:to="`/alunos/${professor.id}`" tag="td" style="cursor: pointer;">
                        {{ professor.nome }} {{ professor.sobrenome }}
                    </router-link>
                    <td class="colPequeno" style="text-align: center; width: 15%;">
                        {{ professor.qtdAlunos }}
                    </td>
                </tr>
            </tbody>
            <tfoot v-if="!professores.length">
                <tr>
                    <td colspan="3">
                        <h4>Nenhum professor encontrado </h4>
                    </td>
                </tr>
            </tfoot>
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
            alunos: [],
            nome: "",
            foto: "",
            cpfBase: "",
            cpfInvalido: false,
            cpfExistente: false
        }
    },
    created() {
        this.$http.get('http://localhost:5000/api/aluno')
            .then(res => res.json())
            .then(alunos => {
                this.alunos = alunos;
                this.carregarProfessores();
            })
    },
    props: {

    },
    methods: {
        openFileDialog() {
            this.$refs.input.value = null;
            this.$refs.input.click();
        },
        handleFile(evento) {
            const file = evento.target.files[0];
            if (file) {
                this.foto = file; // Armazena o arquivo selecionado
            }
        },
        pegarQtdAlunos() {
            this.professores.forEach((professor, index) => {
                professor = {
                    id: professor.id,
                    nome: professor.nome,
                    sobrenome: professor.sobrenome,
                    cpf: professor.cpf,
                    qtdAlunos: this.alunos.filter(aluno =>
                        aluno.professor.id == professor.id
                    ).length
                }
                console.log(professor),
                    this.professores[index] = professor;
            });
        },
        carregarProfessores() {
            this.$http
                .get('http://localhost:5000/api/professor')
                .then(res => res.json())
                .then(professor => {
                    this.professores = professor
                    this.pegarQtdAlunos();
                })
        },

        adicionarProfessor() {
            this.cpfInvalido = false;
            this.cpfExistente = false;
            
            this.openFileDialog();

            this.$refs.input.addEventListener("change", () => {
                if (!this.foto) {
                    alert("Por favor, selecione uma imagem.");
                    return;
                }

                if (this.foto) {
                    const formData = new FormData();
                    formData.append("nome", this.nome);
                    formData.append("foto", this.foto);
                    formData.append("cpf", this.cpfBase);

                    this.$http.post('http://localhost:5000/api/professor', formData, {
                    })
                        .then(res => {
                            if (res.ok) { 
                                return res.json();
                            } else {
                                return res.text().then(textErrorMessage => {
                                    const error = new Error(textErrorMessage || `Erro HTTP ${res.status}`);
                                    error.status = res.status; 
                                    error.body = textErrorMessage; 
                                    throw error;
                                });
                            }
                        })
                        .then(profRetornado => { 
                            this.professores.push(profRetornado);
                            this.nome = '';
                            this.foto = null;
                            this.$refs.input.value = null; 
                            this.cpfBase = '';
                            this.pegarQtdAlunos();
                        })
                        .catch(error => {
                            console.error("Erro capturado:", error);
                            this.foto = null; 
                            this.$refs.input.value = null; 

                            let mensagemDeErro = "";
                            let errorStatus = null;

                            if (error.body) {
                                mensagemDeErro = error.body;
                            } 

                            if (error.status) {
                                errorStatus = error.status;
                            }

                            if (errorStatus === 400 && mensagemDeErro.toLowerCase().includes('erro 02')) {
                                this.cpfExistente = true;
                            } else if (errorStatus === 400 && mensagemDeErro.toLowerCase().includes('erro 01')) {
                                this.cpfInvalido = true;
                            } 
                        });
                }
            }, { once: true });
        },
        formatarCPF(valor) {
            // Remove tudo que não for número
            valor = valor.replace(/\D/g, '');

            // Limita a 11 dígitos
            valor = valor.substring(0, 11);

            // Aplica a máscara: 000.000.000-00
            if (valor.length > 9) {
                valor = valor.replace(/(\d{3})(\d{3})(\d{3})(\d{1,2})/, "$1.$2.$3-$4");
            } else if (valor.length > 6) {
                valor = valor.replace(/(\d{3})(\d{3})(\d{1,3})/, "$1.$2.$3");
            } else if (valor.length > 3) {
                valor = valor.replace(/(\d{3})(\d{1,3})/, "$1.$2");
            }
            return valor;
        }
    }
}
</script>

<style scoped>
input {
    width: calc(100% - 200px);
    border: 0;
    padding: 10px;
    font-size: 1.1em;
    color: rgb(66, 66, 66);
    margin-right: 10px;
    display: inline;
}

.btnInput {
    width: 160px;
    background-color: lightgreen;
    border: 1.5px solid black;
    padding: 10px;
    font-size: 1.1em;
    display: inline;
}

.btnInput:hover {
    border: 1.5px solid black;
    margin: 0;
}

.divInput {
    padding-bottom: 8px;
}
</style>
