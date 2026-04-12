#include<stdio.h>

struct no {
    int info;         
    struct no * prox;
};

struct no * novoNo(int info) {
    struct no * novo = malloc(sizeof(struct no));
    novo->info = info;
    return novo;
}

struct no * insert_first(struct no * lista, int info) {
    struct no *novo = novoNo(info);
    if (!novo) return lista;   // se falhar, mantém a lista como estava
    novo->prox = lista;        // novo aponta para a antiga cabeça
    return novo;               // novo vira a cabeça
}

void insert_last(struct no * lista, int info) {
    struct no *novo = novoNo(info);
    // Precisamos encontrar o último elemento da lista. Quando encontramos, adicionamos o novo!
    struct no *curr = lista;
    while (curr->prox != NULL) {
        curr = curr->prox;
    }
    curr->prox = novo;
}

struct no * remove_first(struct no * lista) {
    if (lista == NULL) return NULL; // lista vazia

    struct no *novo_inicio = lista->prox;
    return novo_inicio;
}

void print_list(struct no *lista){

    struct no *curr;

    for(curr = lista ; curr != NULL ; curr = curr->prox){
        printf("%d => " , curr->info);
    }
    printf("NULL\n");
}

void remove_last(struct no * lista){

    struct no *curr;

    for(curr = lista ; curr->prox->prox != NULL ; curr = curr->prox);

    curr->prox = NULL;
}
//cria curr como um ponteiro "atual" apontando pro começo da fila e faz ele passar pela fila toda ate o penultimo
//quando ele chega no penultimo ele faz o curr->prox ser nulo, ou seja, apaga o ultimo elemento da fila

int main() {

    struct no * lista = NULL;

    for (int i = 100 ; i >= 10 ; i-=10)
        lista = insert_first(lista , i);
    
    print_list(lista);

    printf("\n LISTA SEM O ULTIMO:\n");

    remove_last(lista);

    print_list(lista);


    return 0;
}