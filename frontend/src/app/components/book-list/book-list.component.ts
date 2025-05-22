import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BookService } from '../../services/book.service';
import { Book } from '../../models/book.model';

@Component({
  selector: 'app-book-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './book-list.component.html',
})
export class BookListComponent {
  
  books: Book[] = [];
  newBook: Book = this.getEmptyBook();
  editMode = false;
  showModal = false;
  errorMessage = '';
  successMessage = '';
  showDeleteConfirm = false;
  bookToDelete: Book | null = null;

  constructor(private bookService: BookService) {}

  ngOnInit() {
    this.loadBooks();
  }

  getEmptyBook(): Book {
    return { 
      id: 0, 
      title: '', 
      author: '', 
      isbn: '', 
      publicationDate: new Date() 
    };
  }

  loadBooks() {
    this.bookService.getBooks().subscribe({
      next: (data) => {
        this.books = data;
      },
      error: (error) => {
        this.showError('Failed to load books. Please refresh the page.');
        console.error('Error loading books:', error);
      }
    });
  }

  openAddModal() {
    this.newBook = this.getEmptyBook();
    this.editMode = false;
    this.showModal = true;
  }

  openEditModal(book: Book) {
    this.newBook = { ...book };
    this.editMode = true;
    this.showModal = true;
  }

  closeModal() {
    this.showModal = false;
    this.newBook = this.getEmptyBook();
    this.editMode = false;
  }

  saveBook() {
    if (this.editMode) {
      this.bookService.updateBook(this.newBook).subscribe({
        next: () => {
          this.loadBooks();
          this.closeModal();
          this.showSuccess('Book updated successfully!');
        },
        error: (error) => {
          this.showError('Failed to update book. Please try again.');
          console.error('Error updating book:', error);
        }
      });
    } else {
      this.bookService.addBook(this.newBook).subscribe({
        next: (response) => {
          this.loadBooks();
          this.closeModal();
          this.showSuccess('Book added successfully!');
        },
        error: (error) => {
          this.showError('Failed to add book. Please try again.');
          console.error('Error adding book:', error);
        }
      });
    }
  }

  deleteBook(id: number) {
    this.bookToDelete = this.books.find(b => b.id === id) || null;
    this.showDeleteConfirm = true;
    this.clearMessages();
  }

  confirmDelete() {
    if (this.bookToDelete) {
      this.showDeleteConfirm = false;
      
      this.bookService.deleteBook(this.bookToDelete.id).subscribe({
        next: () => {
          this.loadBooks();
          this.showSuccess('Book deleted successfully!');
          this.bookToDelete = null;
        },
        error: (error) => {
          this.showError('Failed to delete book. Please try again.');
          this.bookToDelete = null;
          console.error('Error deleting book:', error);
        }
      });
    }
  }

  cancelDelete() {
    this.showDeleteConfirm = false;
    this.bookToDelete = null;
  }

  formatDateForInput(date: Date): string {
    if (!date) return '';
    const d = new Date(date);
    return d.toISOString().split('T')[0];
  }

  formatDateForDisplay(date: Date): string {
    if (!date) return '';
    return new Date(date).toLocaleDateString();
  }

  onDateChange(dateString: string) {
    this.newBook.publicationDate = new Date(dateString);
  }

  showSuccess(message: string) {
    this.successMessage = message;
    this.errorMessage = '';
  }

  showError(message: string) {
    this.errorMessage = message;
    this.successMessage = '';
  }

  clearMessages() {
    this.errorMessage = '';
    this.successMessage = '';
  }

  dismissMessage() {
    this.clearMessages();
  }
}