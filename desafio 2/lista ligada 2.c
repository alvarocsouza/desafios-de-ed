#include<stdio.h>
#include <stdlib.h>
#include <time.h>

/** Definição de estruturas **/

typedef struct no {
    int info;
    struct no * prox;
} No;

typedef No * Celula;

typedef struct lista {
    Celula inicio;
    Celula fim;
} Lista;

typedef Lista * ListaLigada;

// ----------------------------- //

/**
 Vamos usar o prefixo ll para todas as funções que são de Lista Ligada (dois l's)
*/

ListaLigada novaLista() {
    ListaLigada l = malloc(sizeof(Lista));
    if (!l) return NULL;
    l->inicio = NULL;
    l->fim = NULL;
    return l;
}

Celula novaCelula(int info) {
    Celula celula = (Celula) malloc(sizeof(No));
    celula->info = info;
    celula->prox = NULL;

    return celula;
}

void llPrint(ListaLigada lista) {
    for (Celula aux = lista->inicio; aux != NULL; aux = aux->prox) {
        printf("%d", aux->info);
        if (aux->prox != NULL)
            printf(" -> ");
    }
    printf("\n");
}

/*
    Busca uma Celula a partir de info, retornando seu ponteiro
*/
Celula llBusca(ListaLigada lista, int info) {
    for (Celula aux = lista->inicio; aux != NULL; aux = aux->prox) {
        if (aux->info == info) {
            return aux;
        }
    }
    return NULL;
}

Celula llGetPenultimo(ListaLigada lista) {
    if (lista->inicio == NULL) {
        return NULL;
    }

    Celula penultimo = NULL;
    for(Celula aux = lista->inicio; aux->prox != NULL; aux = aux->prox) {
        penultimo = aux;
    }
    return penultimo;
}

void llInsereNoInicio(ListaLigada lista, int info) {
    Celula celula = novaCelula(info);
    if (lista->inicio == NULL) { // lista é vazia
        lista->inicio = celula;
        lista->fim = celula;
    }
    else {
        celula->prox = lista->inicio;
        lista->inicio = celula;
    }
}

void llInsereNoFim(ListaLigada lista, int info) {
    Celula nova = novaCelula(info);
    
    if (lista->inicio == NULL) { // ou seja, a lista está vazia
        lista->inicio = nova;
        lista->fim = nova;
        return;
    }
    lista->fim->prox = nova;
    lista->fim = nova;
}

void llRemoveInicio(ListaLigada lista) {
    if (lista->inicio == NULL) {
        return;
    }

    Celula removido = lista->inicio;
    lista->inicio = removido->prox;
    free(removido); // liberando o ponteiro da Celula removida
}

void llInsereDepoisDe(ListaLigada lista, int alvo, int info) {
    Celula nova = novaCelula(info);
    Celula buscada = llBusca(lista, alvo);
    if (buscada != NULL) { // encontrou!
        nova->prox = buscada->prox;
        buscada->prox = nova;
    }
    else {
        llInsereNoFim(lista, info);
        free(nova);
    }
}

void llInsereAntesDe(ListaLigada lista, int alvo, int info) {
    Celula nova = novaCelula(info);
    Celula aux = NULL;
    for (aux = lista->inicio; aux->prox != NULL && aux->prox->info != alvo; aux = aux->prox);
    if (aux->prox != NULL) { // encontrou!
        nova->prox = aux->prox;
        aux->prox = nova;
    }
    else {
        llInsereNoInicio(lista, info);
        free(nova);
    }
}

/*
    Remove uma Celula específica da Lista, baseado em seu valor info
*/
Celula llRemove(ListaLigada lista, int alvo) {
    Celula aux = NULL;
    for (aux = lista->inicio; aux->prox != NULL && aux->prox->info != alvo; aux = aux->prox);
    if (aux != NULL) { // aux é a celula anterior ao no que quero remover
        Celula removido = aux->prox;
        aux->prox = aux->prox->prox;
        free(removido);
    }
    
    return NULL; // Não consegui fazer nada, retorna nulo...
}

void llRemoveFim(ListaLigada lista) {
    Celula penultimo = llGetPenultimo(lista);
    Celula removido = lista->fim;
    lista->fim = penultimo;
    penultimo->prox = NULL;
    free(removido);
}

void BubbleSort(ListaLigada lista){

    if (lista->inicio == NULL) {
        return;
    }

    int temp = 0;

    for(Celula i = lista->inicio; i != NULL; i = i->prox){

        for(Celula aux = lista->inicio ; aux->prox != NULL ; aux = aux->prox){
            if(aux->info > aux->prox->info){
               temp = aux->info;
                aux->info = aux->prox->info;
                aux->prox->info = temp;
            }
        }
    }
}


int main() {
    ListaLigada lista = novaLista();
    srand(time(NULL));
   
    for (int i = 1; i <= 10; i++) {
        int ale = rand() % 100+1;
        llInsereNoFim(lista, ale);
    }

    printf("Lista: \n");
    llPrint(lista);

    BubbleSort(lista);
    printf("Lista Ordernada: \n");
    llPrint(lista);

    return 0;
}