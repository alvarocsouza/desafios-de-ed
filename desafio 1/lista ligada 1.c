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
    if (!novo) return lista;  
    novo->prox = lista;        
    return novo;               
}

void insert_last(struct no * lista, int info) {
    struct no *novo = novoNo(info);
    
    struct no *curr = lista;
    while (curr->prox != NULL) {
        curr = curr->prox;
    }
    curr->prox = novo;
}

struct no * remove_first(struct no * lista) {
    if (lista == NULL) return NULL;

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
//cria um ponteiro chamado curr q mostra qual elemento é o atual, faz ele passar pela lista imprimindo o curr->info


int main() {

    struct no * lista = NULL;

    for (int i = 10 ; i <= 100 ; i+=10)
        lista = insert_first(lista , i);
    
    print_list(lista);

    return 0;
}