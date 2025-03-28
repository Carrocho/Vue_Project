<template>
    <div class="barraTop">
        <nav class="margemPrincipal">
            <router-link to="/professores">Professores</router-link>
            <router-link to="/alunos">Alunos</router-link>
            <router-link to="/sobre">Sobre</router-link>
        </nav>
    </div>
</template>

<script>
export default {

}
</script>

<style scoped>
.barraTop {
    padding: 15px;
    background-color: rgb(177, 177, 177);
}

nav .router-link-active{
    background-color: rgb(49, 117, 235);
    color:aliceblue
}

nav a {
    padding: 9px;
    text-decoration: none;
    background-color: aliceblue;
    border-radius: 3px;
    color: rgb(73, 73, 255);
    font-weight: bold;
    margin-right: 8px;
}
</style>